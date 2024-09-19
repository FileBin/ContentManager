using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Security.Services.Guards;

public class AuthorizedResourceReadGuard<T>(IUserContextAccessor userContextAccessor)
: IEntityReadGuard<T> where T : class, IAuthorizedResource {
    public IQueryable<T> FilterQuery(IQueryable<T> query) {
        var userId = userContextAccessor.GetUserId();

        if (userId == null) {
            return query.Where(g => g.IsAnonymousAccessible());
        }

        return query
            .Include(g => g.ReaderGroup)
                .ThenInclude(g => g!.Users)
            .Include(g => g.EditorGroup)
                .ThenInclude(g => g!.Users)
            .Include(g => g.OwnerGroup)
                .ThenInclude(g => g!.Users)
            .AsTracking()
            .Where(g => g.CanUserViewResource(userId));
    }
}
