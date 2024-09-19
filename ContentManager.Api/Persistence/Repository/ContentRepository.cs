using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentRepository(ApplicationContext context)
: EntityCrudRepositoryBase<Content>, IContentRepository {
    protected override DbSet<Content> GetDbSet() => context.Contents;
    protected override IQueryable<Content> StartQuery() => context.Contents;
}
