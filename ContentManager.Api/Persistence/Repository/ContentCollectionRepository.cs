using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentCollectionRepository(ApplicationContext context)
: EntityCrudRepositoryBase<ContentCollection>, IContentCollectionRepository {
    protected override DbSet<ContentCollection> GetDbSet() => context.ContentCollections;
    protected override IQueryable<ContentCollection> StartQuery() => context.ContentCollections;
}
