using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

namespace ContentManager.Api.Helpers.Extensions;

public static class AuthorizedResourceExtensions {
    public static bool IsUserInReaderGroup<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.ReaderGroupName != null && userGroups.Contains(resource.ReaderGroupName);
    }

    public static bool IsUserInEditorGroup<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.EditorGroupName != null && userGroups.Contains(resource.EditorGroupName);
    }

    public static bool IsUserInOwnerGroup<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.OwnerGroupName != null && userGroups.Contains(resource.OwnerGroupName);
    }

    public static bool IsUserOwner<T>(this T resource, string userId) where T : IAuthorizedResource {
        return resource.OwnerUserId == userId;
    }

    public static bool IsAnonymousAccessible<T>(this T resource) where T : IAuthorizedResource {
        return resource.IsPublic && !resource.IsDraft;
    }

    public static IQueryable<T> FilterViewAnonymousResources<T>(this IQueryable<T> query) where T : IAuthorizedResource {
        return query.Where(r => r.IsPublic && !r.IsDraft);
    }

    public static IQueryable<T> FilterViewUserResources<T>(this IQueryable<T> query, string userId) where T : IAuthorizedResource {
        return query.Where(
            r => r.IsPublic && !r.IsDraft
              || r.OwnerUserId == userId);
    }

    public static IQueryable<T> FilterViewUserResources<T>(this IQueryable<T> query, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return query.Where(
            r => r.IsPublic && !r.IsDraft
              || r.OwnerUserId == userId
              || r.ReaderGroupName != null && userGroups.Contains(r.ReaderGroupName)
              || r.EditorGroupName != null && userGroups.Contains(r.EditorGroupName)
              || r.OwnerGroupName != null && userGroups.Contains(r.OwnerGroupName));
    }

    public static bool CanUserEditResource<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.IsUserOwner(userId)
        || resource.IsUserInEditorGroup(userId, userGroups)
        || resource.IsUserInOwnerGroup(userId, userGroups);
    }

    public static bool CanUserDeleteResource<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.IsUserOwner(userId)
        || resource.IsUserInOwnerGroup(userId, userGroups);
    }

    public static bool CanUserGrantResource<T>(this T resource, string userId, IEnumerable<string> userGroups) where T : IAuthorizedResource {
        return resource.IsUserOwner(userId)
        || resource.IsUserInOwnerGroup(userId, userGroups);
    }
}
