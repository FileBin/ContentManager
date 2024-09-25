using System.Net.Http.Headers;
using System.Net.Mime;
using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Application.Services;
using ContentManager.Api.Contracts.Domain;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Helpers.Extensions;
using Filebin.Shared.Exceptions.Models;
using Filebin.Shared.Misc.Repository;
using Microsoft.AspNetCore.Http;

namespace ContentManager.Api.Application.Services;

internal class ContentPostContentService(
    IFileStorageService fileStorage,
    ISecureContentPostRepository securePostRepository,
    ISecureContentRepository secureContentRepository,
    ISecureUnitOfWork secureUnitOfWork,
    ICancellationTokenObtainer cancellationTokenObtainer)
: IContentPostContentService {
    public async Task<Guid> UploadContentAsync(IFormFile file, Guid postId, int? postOrder = null, int? postVariant = null) {
        await securePostRepository.EnsureExistsAsync(postId, CancellationToken);

        var fileStream = file.OpenReadStream();

        var contentId = Guid.NewGuid();

        //TODO add file processing
        var contentType = fileStream.GetContentType();

        var uniquePostfix = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

        var filename = Path.GetFileNameWithoutExtension(file.FileName);
        var ext = Path.GetExtension(file.FileName);

        if (filename.Length > DefaultConstraints.MaxFilenameLength) {
            filename = filename[..DefaultConstraints.MaxFilenameLength];
        }

        filename = $"{postId}/{filename}_{uniquePostfix}.{ext}";

        var content = new Content() {
            Id = contentId,
            PostId = postId,
            ContentType = contentType,
            LocalFilePath = filename,
            PostOrder = postOrder ?? await secureContentRepository.GetMaxPostOrderAsync(postId, postVariant ?? 1) + 1,
        };

        if (postVariant.HasValue) {
            content.PostVariant = postVariant.Value;
        }

        secureContentRepository.Create(content);
        var stream = fileStorage.WriteFile(filename).BaseStream;

        var databaseTask = secureUnitOfWork.SaveChangesAsync(CancellationToken);

        var fileTask = fileStream
            .CopyToAsync(stream)
            .ContinueWith(task => stream.Flush())
            .ContinueWith(task => stream.Close());

        await Task.WhenAll(databaseTask, fileTask);

        return contentId;
    }

    public async Task<FileResponse> DownloadContentAsync(Guid contentId) {
        var content = await secureContentRepository.GetByIdOrThrow(contentId, CancellationToken);

        return Download(content);
    }

    public async Task<FileResponse> DownloadContentByPostIdAsync(Guid postId, int postOrder, int? postVariant = null) {
        var content = await secureContentRepository
            .GetContentByPostIdOrderAndVariantAsync(postId, postOrder, postVariant ?? 1, CancellationToken)
            ?? throw new NotFoundException("Content with given parameters wasn't found");

        return Download(content);
    }

    private FileResponse Download(Content content) {
        var ext = Path.GetExtension(content.LocalFilePath);

        /*
        TODO improve name generation using tags (author, character) 
               maybe make a group of tags that will be included into filename
        */

        var filename = string.Concat(content.ContentPost.Name, ext);

        var stream = fileStorage.ReadFile(content.LocalFilePath).BaseStream;

        return new FileResponse {
            FileStream = stream,
            FileName = filename,
        };
    }


    private CancellationToken CancellationToken => cancellationTokenObtainer.GetCancellationToken();
}