using System;
using ContentManager.Api.Contracts.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Data;

internal class DbContextAccessor(ApplicationContext applicationContext) : IDbContextAccessor {
    public DbContext GetApplicationContext() => applicationContext;
}
