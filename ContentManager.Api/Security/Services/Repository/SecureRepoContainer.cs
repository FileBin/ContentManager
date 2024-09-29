using ContentManager.Api.Contracts.Security.Repository;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Security.Services.Repository;

public class SecureRepoContainer<TRepository, T> : ISecureRepoContainer<TRepository, T>
where TRepository : IRepository<T>
where T : class {
    private readonly TRepository _repository;
    public SecureRepoContainer(ISecureEntityObtainer secureEntityObtainer, TRepository repository) {
        _repository = repository;
        _repository.UseObtainerAsDefault(secureEntityObtainer);
    }

    public TRepository Repo => _repository;
}
