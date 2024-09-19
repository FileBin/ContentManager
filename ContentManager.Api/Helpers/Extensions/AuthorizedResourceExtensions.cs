using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

namespace ContentManager.Api.Helpers.Extensions;

public static class AuthorizedResourceExtensions {
    public static bool IsUserInReaderGroup(this IAuthorizedResource resource, string userId) {
        return resource.ReaderGroup != null && resource.ReaderGroup.Users.Any(u => u.Id == userId);
    }

    public static bool IsUserInEditorGroup(this IAuthorizedResource resource, string userId) {
        return resource.EditorGroup != null && resource.EditorGroup.Users.Any(u => u.Id == userId);
    }

    public static bool IsUserInOwnerGroup(this IAuthorizedResource resource, string userId) {
        return resource.OwnerGroup != null && resource.OwnerGroup.Users.Any(u => u.Id == userId);
    }

    public static bool IsUserOwner(this IAuthorizedResource resource, string userId) {
        return resource.OwnerUserId == userId;
    }

    public static bool IsAnonymousAccessible(this IAuthorizedResource resource) {
        return resource.IsPublic && !resource.IsDraft;
    }

    public static bool CanUserViewResource(this IAuthorizedResource resource, string userId) {
        return resource.IsAnonymousAccessible()
        || resource.IsUserOwner(userId)
        || resource.IsUserInReaderGroup(userId)
        || resource.IsUserInEditorGroup(userId)
        || resource.IsUserInOwnerGroup(userId);
    }

    public static bool CanUserEditResource(this IAuthorizedResource resource, string userId) {
        return resource.IsUserOwner(userId)
        || resource.IsUserInEditorGroup(userId)
        || resource.IsUserInOwnerGroup(userId);
    }

    public static bool CanUserDeleteResource(this IAuthorizedResource resource, string userId) {
        return resource.IsUserOwner(userId)
        || resource.IsUserInOwnerGroup(userId);
    }

    public static bool CanUserGrantResource(this IAuthorizedResource resource, string userId) {
        return resource.IsUserOwner(userId)
        || resource.IsUserInOwnerGroup(userId);
    }
}
