namespace ContentManager.Api.Contracts.Security.Services;

public interface IUserContextAccessor {
    public string? GetUserId();
}