using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;

namespace ContentManager.Api.Contracts.Security.Repository;

public interface ISecureContentRepository : IContentRepository {
    Task<int> GetMaxPostOrderAsync(Guid postId, int postVariant = 1, CancellationToken cancellationToken = default);
    Task<Content?> GetContentByPostIdOrderAndVariantAsync(Guid postId, int postOrder, int postVariant = 1, CancellationToken cancellationToken = default);
}
