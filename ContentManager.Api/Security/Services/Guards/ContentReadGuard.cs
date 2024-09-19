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
            return query.Where(c => c.ContentPost.IsAnonymousAccessible());
        }

        return query
            .Include(c => c.ContentPost.ReaderGroup)
                .ThenInclude(g => g!.Users)
            .Include(c => c.ContentPost.EditorGroup)
                .ThenInclude(g => g!.Users)
            .Include(c => c.ContentPost.OwnerGroup)
                .ThenInclude(g => g!.Users)
            .AsTracking()
            .Where(c => c.ContentPost.CanUserViewResource(userId));
    }
}
