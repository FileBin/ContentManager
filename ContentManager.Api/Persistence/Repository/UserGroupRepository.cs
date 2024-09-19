using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class UserGroupRepository(ApplicationContext context)
: EntityCrudRepositoryBase<UserGroup>, IUserGroupRepository {
    protected override DbSet<UserGroup> GetDbSet() => context.UserGroups;
    protected override IQueryable<UserGroup> StartQuery() => context.UserGroups;

}
