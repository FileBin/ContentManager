using ContentManager.Api.Contracts.Domain;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Exceptions;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class TagRepository(ApplicationContext context) : CrudRepositoryBase<Tag>, ITagRepository {
    protected override DbSet<Tag> GetDbSet() => context.Tags;
    protected override IQueryable<Tag> StartQuery() {
        return context.Tags
            .Include(t => t.Parent)
            .Include(t => t.ContentPosts)
            .AsTracking();
    }

    public Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default) {
        name = name.Trim();
        return StartQuery()
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
    }

    public Task<Tag> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default) {
        return GetOrCreateByNameAsync(name, DefaultConstraints.TagMaxDepth, cancellationToken);
    }

    private async Task<Tag> GetOrCreateByNameAsync(string name, int tagDepth, CancellationToken cancellationToken) {
        if (tagDepth <= 0)
            throw new TagDepthException(DefaultConstraints.TagMaxDepth);

        name = name.Trim();
        return (await StartQuery()
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken))
            ??
            GetDbSet().Add(await CreateTagFromName(name, tagDepth, cancellationToken)).Entity;
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