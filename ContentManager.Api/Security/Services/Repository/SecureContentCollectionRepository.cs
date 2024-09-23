using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureContentCollectionRepository(IDbContextAccessor accessor, IEntityReadGuard<ContentCollection> readGuard)
: EntityCrudRepositoryBase<ContentCollection>, ISecureContentCollectionRepository {
    protected override DbSet<ContentCollection> GetDbSet() {
        return accessor.GetApplicationContext().ContentCollections;
    }

    protected override IQueryable<ContentCollection> StartQuery() {
        return readGuard.FilterQuery(GetDbSet());
    }
}
