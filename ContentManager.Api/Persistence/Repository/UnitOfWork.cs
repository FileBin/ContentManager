using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Persistence.Repository;

internal class UnitOfWork(ApplicationContext context) : IUnitOfWork {
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return context.SaveChangesAsync(cancellationToken);
    }
}