using ContentManager.Api.Contracts.Domain.Data.Models;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Persistance.Repository;

public interface IContentRepository : IEntityRepository<Content> {
    Task<int> GetMaxPostOrderAsync(Guid postId, int postVariant = 1, CancellationToken cancellationToken = default);
    Task<Content?> GetContentByPostIdOrderAndVariantAsync(Guid postId, int postOrder, int postVariant = 1, CancellationToken cancellationToken = default);
}
