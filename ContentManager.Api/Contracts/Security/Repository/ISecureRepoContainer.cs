using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Security.Repository;

public interface ISecureRepoContainer<TRepository, T>
where TRepository : IRepository<T>
where T : class {
    TRepository Repo { get; }
}