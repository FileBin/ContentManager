using ContentManager.Api.Contracts.Security.Services;

namespace ContentManager.Api.Security.Helpers;

public abstract class TypedWriteGuard<T> : IEntityWriteGuard where T : class {
    public abstract bool CanCreate(T entity);
    public abstract bool CanDelete(T entity);
    public abstract bool CanUpdate(T entity, T original);

    public bool CanCreate(object entity) {
        if (entity is not T obj) {
            return true;
        }
        return CanCreate(obj);
    }

    public bool CanDelete(object entity) {
        if (entity is not T obj) {
            return true;
        }
        return CanDelete(obj);
    }

    public bool CanUpdate(object entity, object original) {
        if (entity is not T obj) {
            return true;
        }

        
        return CanUpdate(obj, (T)original);
    }
}
