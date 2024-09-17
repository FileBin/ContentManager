using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class UnitOfWork(ApplicationContext context) : UnitOfWorkBase {
    public override DbContext GetDbContext() => context;
}