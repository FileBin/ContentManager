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
            return query.FilterViewAnonymousResources();
        }

        var userGroups = userContextAccessor.GetUserGroups();

        if (!userGroups.Any()) {
            return query
                .AsTracking().FilterViewUserResources(userId);
        }

        return query
            .AsTracking().FilterViewUserResources(userId, userGroups);
    }
}
