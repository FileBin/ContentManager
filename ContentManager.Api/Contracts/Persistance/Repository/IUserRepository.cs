using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Persistance.Repository;

public interface IUserRepository : IRepository<User> {
    public Task<User?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
