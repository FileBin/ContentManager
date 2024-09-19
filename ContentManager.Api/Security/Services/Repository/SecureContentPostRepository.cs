using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureContentPostRepository(IApplicationContext context, IEntityReadGuard<ContentPost> readGuard)
: EntityCrudRepositoryBase<ContentPost>, ISecureContentPostRepository {
    protected override DbSet<ContentPost> GetDbSet() {
        return context.ContentPosts;
    }

    protected override IQueryable<ContentPost> StartQuery() {
        return readGuard.FilterQuery(context.ContentPosts);
    }
}
