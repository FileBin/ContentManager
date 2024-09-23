using System.Security.Claims;
using ContentManager.Api.Contracts.Security.Services;
using Microsoft.AspNetCore.Http;


namespace ContentManager.Api.Presentation.Services;

public class UserContextAccessor(IHttpContextAccessor httpContextAccessor) : IUserContextAccessor {
    public string? GetUserId() {
        return httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;
    }

    public IReadOnlyCollection<string> GetUserGroups() {
        return []; //TODO implement user groups obtaining
    }
}
