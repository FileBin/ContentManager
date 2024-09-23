namespace ContentManager.Api.Contracts.Persistance.Data;

public interface IDbContextAccessor
{
    ApplicationAbstractContext GetApplicationContext();
}
