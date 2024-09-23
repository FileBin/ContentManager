using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Exceptions.Models;

namespace ContentManager.Api.Helpers.Extensions;

public static class UserContextAccessorExtensions
{
    public static string GetUserIdOrThrow(this IUserContextAccessor accessor) {
        return accessor.GetUserId() ?? throw new ForbiddenException(); //TODO change to unauthorized
    }
}
