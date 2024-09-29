using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentRepository(IEntityAccessor accessor, IEntityObtainer obtainer)
: EntityCrudRepositoryBase<Content>(accessor, obtainer), IContentRepository {
    public Task<int> GetMaxPostOrderAsync(Guid postId, int postVariant = 1, CancellationToken cancellationToken = default) {
        return DbSet
            .Where(c => c.PostId == postId && c.PostVariant == postVariant)
            .Select(c => c.PostOrder)
            .OrderBy(o => o)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Content?> GetContentByPostIdOrderAndVariantAsync(Guid postId, int postOrder, int postVariant = 1, CancellationToken cancellationToken = default) {
        return Query
            .Where(c => c.PostId == postId && c.PostOrder == postOrder && c.PostVariant == postVariant)
            .SingleOrDefaultAsync(cancellationToken);
    }
}

