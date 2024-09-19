using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureUserGroupRepository(IApplicationContext context, IEntityReadGuard<UserGroup> readGuard)
: EntityCrudRepositoryBase<UserGroup>, ISecureUserGroupRepository {
    protected override DbSet<UserGroup> GetDbSet() {
        return context.UserGroups;
    }

    protected override IQueryable<UserGroup> StartQuery() {
        return readGuard.FilterQuery(context.UserGroups);
    }
}
