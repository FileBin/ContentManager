using System.Net.Http.Headers;
using ContentManager.Api.Contracts.Application.Services;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Enum;
using ContentManager.Api.Contracts.Domain.Exceptions;
using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Helpers.Extensions;
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
    public async Task UploadContentAsync(IFormFile file, Guid contentPostId, int postOrder, int? postVariant) {
        await securePostRepository.EnsureExistsAsync(contentPostId, CancellationToken);

        var fileStream = file.OpenReadStream();

        var contentId = Guid.NewGuid();

        //TODO add file processing
        var contentType = fileStream.GetContentType();

        var uniquePostfix = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

        var filename = Path.GetFileNameWithoutExtension(file.FileName);
        var ext = Path.GetExtension(file.FileName);

        filename = $"{contentPostId}/{filename}_{uniquePostfix}.{ext}";

        var content = new Content() {
            Id = contentId,
            PostId = contentPostId,
            ContentType = contentType,
            LocalFilePath = filename,
            PostOrder = postOrder,
            PostVariant = postVariant ?? 0,
        };

        secureContentRepository.Create(content);
        await secureUnitOfWork.SaveChangesAsync(CancellationToken);

        var stream = fileStorage.WriteFile(filename).BaseStream;
        await fileStream.CopyToAsync(stream);
        await stream.FlushAsync();
        stream.Close();
    }

    public async Task<StreamContent> DownloadContentAsync(Guid contentId) {
        var content = await secureContentRepository.GetByIdOrThrow(contentId, CancellationToken);

        var ext = Path.GetExtension(content.LocalFilePath);

        /*
        TODO improve name generation using tags (author, character) 
               maybe make a group of tags that will be included into filename
        */
        var filename = $"{content.ContentPost.Name}.{ext}";

        var stream = fileStorage.ReadFile(content.LocalFilePath).BaseStream;
        var streamContent = new StreamContent(stream);

        streamContent.Headers.ContentDisposition!.FileName = filename;
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        streamContent.Headers.ContentLength = stream.Length;

        return streamContent;
    }

    private CancellationToken CancellationToken => cancellationTokenObtainer.GetCancellationToken();
}