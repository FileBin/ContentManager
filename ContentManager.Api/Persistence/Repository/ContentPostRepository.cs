using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentPostRepository(ApplicationContext context) 
: EntityCrudRepositoryBase<ContentPost>, IContentPostRepository {
    protected override DbSet<ContentPost> GetDbSet() => context.ContentPosts;

    protected override IQueryable<ContentPost> StartQuery() => context.ContentPosts;
}
