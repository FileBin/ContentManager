namespace ContentManager.Api.Contracts.Security.Services;

public interface IEntityWriteGuard
{
    bool CanCreateEntity(object entity);
    bool CanUpdateEntity(object entity, object original);
    bool CanDeleteEntity(object entity);
}
