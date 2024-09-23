namespace ContentManager.Api.Contracts.Security.Services;

public interface IUserContextAccessor {
    public string? GetUserId();
    public IReadOnlyCollection<string> GetUserGroups();
}