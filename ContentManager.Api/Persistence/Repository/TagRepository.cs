using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class TagRepository(ApplicationContext context) : CrudRepositoryBase<Tag>, ITagRepository {
    protected override DbSet<Tag> GetDbSet() => context.Tags;

    public Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default) {
        return GetDbSet()
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
    }

}