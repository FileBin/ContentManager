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

        return entity.CanUserDeleteResource(userId);
    }

    public override bool CanUpdate(IAuthorizedResource entity, IAuthorizedResource original) {
        var userId = userContextAccessor.GetUserId();
        if (userId == null)
            return false;

        if (entity.OwnerGroupId != original.OwnerGroupId && !original.IsUserInOwnerGroup(userId))
            return false;

        if (entity.EditorGroupId != original.EditorGroupId && !original.CanUserGrantResource(userId))
            return false;

        if (entity.ReaderGroupId != original.ReaderGroupId && !original.CanUserGrantResource(userId))
            return false;

        if (entity.IsDraft != original.IsDraft && !original.CanUserGrantResource(userId))
            return false;

        if (entity.IsPublic != original.IsPublic && !original.CanUserGrantResource(userId))
            return false;

        return original.CanUserEditResource(userId);
    }
}
