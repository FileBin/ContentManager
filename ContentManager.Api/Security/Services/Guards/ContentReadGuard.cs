using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Guards;

public class ContentReadGuard(IUserContextAccessor userContextAccessor)
: IEntityReadGuard<Content> {
    public IQueryable<Content> FilterQuery(IQueryable<Content> query) {
        var userId = userContextAccessor.GetUserId();

        query = query.Include(c => c.ContentPost);

        if (userId == null) {
            return query.Where(r => r.ContentPost.IsPublic && !r.ContentPost.IsDraft);
        }

        var userGroups = userContextAccessor.GetUserGroups();

        if (!userGroups.Any()) {
            return query
                .AsTracking()
                .Where(r => r.ContentPost.IsPublic && !r.ContentPost.IsDraft
                         || r.ContentPost.OwnerUserId == userId);
        }

        return query
            .AsTracking()
            .Where(r => r.ContentPost.IsPublic && !r.ContentPost.IsDraft
                    || r.ContentPost.OwnerUserId == userId
                    || r.ContentPost.ReaderGroupName != null && userGroups.Contains(r.ContentPost.ReaderGroupName)
                    || r.ContentPost.EditorGroupName != null && userGroups.Contains(r.ContentPost.EditorGroupName)
                    || r.ContentPost.OwnerGroupName != null && userGroups.Contains(r.ContentPost.OwnerGroupName));
    }
}
