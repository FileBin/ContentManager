using ContentManager.Api.Contracts.Domain.Data.Models;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Data;

internal class EntityObtainer(IEntityAccessor accessor) : IEntityObtainer {

    public IQueryable<T> StartQuery<T>() where T : class {
        IQueryable<T> query = accessor.GetDbSet<T>().AsTracking();

        if (query is IQueryable<Content> qContent) {
            query = (IQueryable<T>)qContent.Include(c => c.ContentPost);
        }

        if (query is IQueryable<ContentPost> qContentPost) {
            query = (IQueryable<T>)qContentPost
                .Include(p => p.Attachments)
                .Include(p => p.Tags)
                .Include(p => p.ContentPostCollections);
        }

        if (query is IQueryable<ContentCollection> qContentCollection) {
            query = (IQueryable<T>)qContentCollection
                .Include(p => p.ContentPostCollections);
        }

        if (query is IQueryable<Tag> qTag) {
            query = (IQueryable<T>)qTag
                .Include(p => p.ContentPosts)
                .Include(p => p.Parent);
        }

        return query;
    }
}
