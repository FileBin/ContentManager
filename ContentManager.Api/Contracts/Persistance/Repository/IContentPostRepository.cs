using ContentManager.Api.Contracts.Domain.Data.Models;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Persistance.Repository;

public interface IContentPostRepository : IEntityRepository<ContentPost> {
    public Task<int> GetCountAsync(CancellationToken cancellationToken = default);
    public Task<IReadOnlyCollection<ContentPost>> GetRecentPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
}
