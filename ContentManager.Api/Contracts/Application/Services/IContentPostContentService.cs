using Microsoft.AspNetCore.Http;

namespace ContentManager.Api.Contracts.Application.Services;

public interface IContentPostContentService {
    Task UploadContentAsync(IFormFile file, Guid contentPostId, int postOrder, int? postVariant);
    Task<StreamContent> DownloadContentAsync(Guid contentId);
}