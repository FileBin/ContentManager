namespace ContentManager.Api.Contracts.Security.Services;

public interface ISecureUnitOfWork {
    public Task<int> ApplyChangesAsync();
}