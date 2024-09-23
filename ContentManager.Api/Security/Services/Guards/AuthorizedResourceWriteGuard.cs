using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Helpers.Extensions;
using ContentManager.Api.Security.Helpers;

namespace ContentManager.Api.Security.Services.Guards;

public class AuthorizedResourceWriteGuard(IUserContextAccessor userContextAccessor) : TypedWriteGuard<IAuthorizedResource> {
    public override bool CanCreate(IAuthorizedResource entity) {
        return userContextAccessor.GetUserId() != null;
    }

    public override bool CanDelete(IAuthorizedResource entity) {
        var userId = userContextAccessor.GetUserId();
        if (userId == null)
            return false;

        var userGroups = userContextAccessor.GetUserGroups();
        
        return entity.CanUserDeleteResource(userId, userGroups);
    }

    public override bool CanUpdate(IAuthorizedResource entity, IAuthorizedResource original) {
        var userId = userContextAccessor.GetUserId();
        if (userId == null)
            return false;

        var userGroups = userContextAccessor.GetUserGroups();

        if (entity.OwnerGroupName != original.OwnerGroupName && !original.IsUserInOwnerGroup(userId, userGroups))
            return false;

        if (entity.EditorGroupName != original.EditorGroupName && !original.CanUserGrantResource(userId, userGroups))
            return false;

        if (entity.ReaderGroupName != original.ReaderGroupName && !original.CanUserGrantResource(userId, userGroups))
            return false;

        if (entity.IsDraft != original.IsDraft && !original.CanUserGrantResource(userId, userGroups))
            return false;

        if (entity.IsPublic != original.IsPublic && !original.CanUserGrantResource(userId, userGroups))
            return false;

        return original.CanUserEditResource(userId, userGroups);
    }
}
