using ContentManager.Api.Contracts.Domain;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Exceptions;
using ContentManager.Api.Contracts.Persistance.Repository;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class TagRepository(IEntityAccessor accessor, IEntityObtainer obtainer)
: CrudRepositoryBase<Tag>(accessor, obtainer), ITagRepository {
    public Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default) {
        name = name.Trim();
        return Query
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
    }

    public Task<Tag> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default) {
        return GetOrCreateByNameAsync(name, DefaultConstraints.TagMaxDepth, cancellationToken);
    }

    private async Task<Tag> GetOrCreateByNameAsync(string name, int tagDepth, CancellationToken cancellationToken) {
        if (tagDepth <= 0)
            throw new TagDepthException(DefaultConstraints.TagMaxDepth);

        name = name.Trim();
        return (await Query
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken))
            ??
            DbSet.Add(await CreateTagFromName(name, tagDepth, cancellationToken)).Entity;
    }

    private async Task<Tag> CreateTagFromName(string name, int tagDepth, CancellationToken cancellationToken) {
        if (tagDepth <= 0)
            throw new TagDepthException(DefaultConstraints.TagMaxDepth);

        name = name.Trim();
        var sepIdx = name.LastIndexOf(':');

        Tag? parentTag = null;

        if (sepIdx != -1) {
            parentTag = await GetOrCreateByNameAsync(name[..sepIdx], tagDepth - 1, cancellationToken);
        }

        return new Tag {
            Name = name,
            Parent = parentTag,
            ParentName = parentTag?.Name,
        };
    }
}