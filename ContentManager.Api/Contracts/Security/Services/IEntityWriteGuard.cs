namespace ContentManager.Api.Contracts.Security.Services;

public interface IEntityWriteGuard
{
    bool CanCreate(object entity);
    bool CanUpdate(object entity, object original);
    bool CanDelete(object entity);
}
