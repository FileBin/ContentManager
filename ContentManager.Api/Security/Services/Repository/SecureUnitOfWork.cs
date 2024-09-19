using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace ContentManager.Api.Security.Services.Repository;

internal class SecureUnitOfWork(
    IDbContextAccessor dbContextAccessor,
    IEnumerable<IEntityWriteGuard> writeEntityGuards,
    IUnitOfWork unitOfWork,
    ILogger<SecureUnitOfWork> logger)
: ISecureUnitOfWork {
    private readonly DbContext context = dbContextAccessor.GetApplicationContext();

    public Task<int> ApplyChangesAsync() {
        var success = context.ChangeTracker.Entries()
            .Select(ValidateChange)
            .All(b => b);

        if (!success) {
            // TODO replace by throw AccessDeniedException
            return Task.FromResult(-1);
        }
        return unitOfWork.SaveChangesAsync();
    }

    private bool ValidateChange(EntityEntry change)
        => change.State switch {
            EntityState.Added => ValidateCreation(change.Entity),
            EntityState.Modified => ValidateUpdate(change.Entity),
            EntityState.Deleted => ValidateDeletion(change.Entity),
            _ => true,
        };


    private bool ValidateCreation(object entity) {
        if (writeEntityGuards.Any(g => !g.CanCreate(entity))) {
            logger.LogError("Access denied to create entity {@Entity}", entity);
            return false;
        }

        return true;
    }

    private bool ValidateUpdate(object entity) {

        var original = context.Entry(entity).OriginalValues.ToObject();

        if (writeEntityGuards.Any(g => !g.CanUpdate(entity, original))) {
            logger.LogError("Access denied to update entity {@Entity}", entity);
            return false;
        }

        return true;
    }

    private bool ValidateDeletion(object entity) {
        if (writeEntityGuards.Any(g => !g.CanDelete(entity))) {
            logger.LogError("Access denied to delete entity {@Entity}", entity);
            return false;
        }

        return true;
    }
}