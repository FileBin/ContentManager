using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureContentRepository(IApplicationContext context, IEntityReadGuard<Content> readGuard)
: EntityCrudRepositoryBase<Content>, ISecureContentRepository {
    protected override DbSet<Content> GetDbSet() {
        return context.Contents;
    }

    protected override IQueryable<Content> StartQuery() {
        return readGuard.FilterQuery(context.Contents);
    }
}
