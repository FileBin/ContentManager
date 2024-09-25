using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureContentRepository(IDbContextAccessor accessor, IEntityReadGuard<Content> readGuard)
: EntityCrudRepositoryBase<Content>, ISecureContentRepository {
    protected override DbSet<Content> GetDbSet() {
        return accessor.GetApplicationContext().Contents;
    }

    protected override IQueryable<Content> StartQuery() {
        return readGuard.FilterQuery(GetDbSet().Include(c => c.ContentPost));
    }

    public Task<int> GetMaxPostOrderAsync(Guid postId, int postVariant = 1, CancellationToken cancellationToken = default) {
        return GetDbSet().Include(c => c.ContentPost)
            .Where(c => c.PostId == postId && c.PostVariant == postVariant)
            .Select(c => c.PostOrder)
            .OrderBy(o => o)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Content?> GetContentByPostIdOrderAndVariantAsync(Guid postId, int postOrder, int postVariant = 1, CancellationToken cancellationToken = default) {
        return StartQuery()
            .Where(c => c.PostId == postId && c.PostOrder == postOrder && c.PostVariant == postVariant)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
