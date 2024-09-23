using ContentManager.Api.Contracts.Persistance.Data;

namespace ContentManager.Api.Persistence.Data;

internal class DbContextAccessor(ApplicationContext applicationContext) : IDbContextAccessor {
    public ApplicationAbstractContext GetApplicationContext() => applicationContext;
}
