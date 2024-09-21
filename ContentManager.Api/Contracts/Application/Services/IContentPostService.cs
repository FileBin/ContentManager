using ContentManager.Api.Contracts.Application.Models.Requests;
using ContentManager.Api.Contracts.Application.Models.Responses;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Application.Services;

public interface IContentPostService {
    Task<ContentPostResponse> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ContentPostResponse>> GetPageAsync(IPageDesc pageDesc);
    Task<Guid> CreateAsync(ContentPostCreateRequest createRequest);
    Task UpdateAsync(Guid id, ContentPostUpdateRequest updateRequest);
    Task DeleteAsync(Guid id);
}
