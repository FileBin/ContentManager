using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Application.Services;
using ContentManager.Api.Contracts.Domain;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Enum;
using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Helpers.Extensions;
using Filebin.Shared.Exceptions.Models;
using Filebin.Shared.Misc.Repository;
using Microsoft.AspNetCore.Http;

namespace ContentManager.Api.Application.Services;

internal class ContentPostContentService(
    IFileStorageService fileStorage,
    IImageProcessingService imageProcessingService,
    ISecureRepoContainer<IContentPostRepository, ContentPost> postContainer,
    ISecureRepoContainer<IContentRepository, Content> contentContainer,
    ISecureUnitOfWork secureUnitOfWork,
    ICancellationTokenObtainer cancellationTokenObtainer)
: IContentPostContentService {
    public async Task<Guid> UploadContentAsync(IFormFile file, Guid postId, int? postOrder = null, int? postVariant = null) {
        await postContainer.Repo.EnsureExistsAsync(postId, CancellationToken);

        var fileStream = file.OpenReadStream();

        var contentId = Guid.NewGuid();

        var contentType = fileStream.GetContentType();

        var uniquePostfix = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

        var filename = Path.GetFileNameWithoutExtension(file.FileName);
        var ext = Path.GetExtension(file.FileName);

        if (filename.Length > DefaultConstraints.MaxFilenameLength) {
            filename = filename[..DefaultConstraints.MaxFilenameLength];
        }

        filename = $"{postId}/{filename}_{uniquePostfix}{ext}";

        var content = new Content() {
            Id = contentId,
            PostId = postId,
            ContentType = contentType,
            LocalFilePath = filename,
            PostOrder = postOrder ?? await contentContainer.Repo.GetMaxPostOrderAsync(postId, postVariant ?? 1) + 1,
        };

        if (postVariant.HasValue) {
            content.PostVariant = postVariant.Value;
        }

        contentContainer.Repo.Create(content);
        var stream = fileStorage.WriteFile(filename).BaseStream;

        var databaseTask = secureUnitOfWork.SaveChangesAsync(CancellationToken);

        var fileTask = fileStream
            .CopyToAsync(stream)
            .ContinueWith(task => stream.Flush())
            .ContinueWith(task => stream.Close());

        await Task.WhenAll(databaseTask, fileTask);

        return contentId;
    }

    public async Task<FileResponse> DownloadContentAsync(Guid contentId, string? previewName = null) {
        var content = await contentContainer.Repo.GetByIdOrThrow(contentId, CancellationToken);
        if (previewName is null) {
            return Download(content);
        } else {
            if (content.ContentType == ContentType.Picture) {
                return await DownloadPreviewAsync(content, previewName);
            }
            throw new NotFoundException($"Previewing {content.ContentType} was not supported");
        }
    }

    public async Task<FileResponse> DownloadContentByPostIdAsync(Guid postId, int postOrder, int? postVariant = null) {
        var content = await contentContainer.Repo
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

    private async Task<FileResponse> DownloadPreviewAsync(Content content, string previewName) {
        var ext = ".jpg";

        var filename = string.Concat(content.ContentPost.Name, "_", previewName, ext);
        using var readStream = fileStorage.ReadFile(content.LocalFilePath).BaseStream;
        var stream = await imageProcessingService.GetPreviewAsync(readStream, previewName);

        return new FileResponse {
            FileStream = stream,
            FileName = filename,
        };
    }


    private CancellationToken CancellationToken => cancellationTokenObtainer.GetCancellationToken();
}