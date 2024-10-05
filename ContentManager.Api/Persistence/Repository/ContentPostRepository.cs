using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentPostRepository(IEntityAccessor accessor, IEntityObtainer obtainer)
: EntityCrudRepositoryBase<ContentPost>(accessor, obtainer), IContentPostRepository {
    public Task<int> GetCountAsync(CancellationToken cancellationToken = default) {
        return Query.CountAsync(cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<ContentPost>> GetRecentPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        return await Query
            .OrderByDescending(p => p.CreatedAt)
            .Paginate(pageDesc)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
