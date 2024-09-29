using ContentManager.Api.Contracts.Application.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace ContentManager.Api.Contracts.Application.Services;

public interface IContentPostContentService {
    Task<Guid> UploadContentAsync(IFormFile file, Guid postId, int? postOrder = null, int? postVariant = null);
    Task<FileResponse> DownloadContentAsync(Guid contentId, string? previewName = null);
    Task<FileResponse> DownloadContentByPostIdAsync(Guid postId, int postOrder, int? postVariant = null);
}