namespace ContentManager.Api.Contracts.Security.Services;

public interface IEntityReadGuard<T> where T : class
{
    bool CanRead(T entity);
    IQueryable<T> FilterQuery(IQueryable<T> query);
}