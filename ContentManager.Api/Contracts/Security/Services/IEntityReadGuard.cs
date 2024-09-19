namespace ContentManager.Api.Contracts.Security.Services;

public interface IEntityReadGuard<T> where T : class
{
    IQueryable<T> FilterQuery(IQueryable<T> query);
}