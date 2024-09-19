using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class UserRepository(ApplicationContext context) : CrudRepositoryBase<User>, IUserRepository {
    protected override DbSet<User> GetDbSet() => context.Users;
    protected override IQueryable<User> StartQuery() => context.Users;

    public Task<User?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default) {
        return GetDbSet()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
}
