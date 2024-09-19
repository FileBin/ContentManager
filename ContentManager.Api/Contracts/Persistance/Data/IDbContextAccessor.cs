using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Persistance.Data;

public interface IDbContextAccessor
{
    DbContext GetApplicationContext();
}
