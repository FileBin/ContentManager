using ContentManager.Api.Contracts.Domain.Data.Models;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Persistance.Repository;

public interface ITagRepository : IRepository<Tag> {
    public Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}