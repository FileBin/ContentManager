using Filebin.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Data;

internal class EntityAccessor(ApplicationContext applicationContext) : IEntityAccessor {
    public DbSet<T> GetDbSet<T>() where T : class {
        return applicationContext.Set<T>();
    }
}
