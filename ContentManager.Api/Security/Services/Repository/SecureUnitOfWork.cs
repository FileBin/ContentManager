﻿using ContentManager.Api.Contracts.Domain.Exceptions;
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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        var success = context.ChangeTracker.Entries()
            .Select(ValidateChange)
            .All(b => b);

        if (!success) {
            throw new WriteAccessDeniedException();
        }
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private bool ValidateChange(EntityEntry change)
        => change.State switch {
            EntityState.Added => ValidateCreation(change.Entity),
            EntityState.Modified => ValidateUpdate(change.Entity),
            EntityState.Deleted => ValidateDeletion(change.Entity),
            _ => true,
        };


    private bool ValidateCreation(object entity) {
        if (writeEntityGuards.Any(g => !g.CanCreateEntity(entity))) {
            logger.LogError("Access denied to create entity {@Entity}", entity);
            return false;
        }

        return true;
    }

    private bool ValidateUpdate(object entity) {

        var original = context.Entry(entity).OriginalValues.ToObject();

        if (writeEntityGuards.Any(g => !g.CanUpdateEntity(entity, original))) {
            logger.LogError("Access denied to update entity {@Entity}", entity);
            return false;
        }

        return true;
    }

    private bool ValidateDeletion(object entity) {
        if (writeEntityGuards.Any(g => !g.CanDeleteEntity(entity))) {
            logger.LogError("Access denied to delete entity {@Entity}", entity);
            return false;
        }

        return true;
    }
}