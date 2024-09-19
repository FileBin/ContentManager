namespace ContentManager.Api.Contracts.Security.Repository;

public interface ISecureUnitOfWork {
    public Task<int> ApplyChangesAsync();
}