using ContentManager.Api.Contracts.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Persistance.Data;

public abstract class ApplicationAbstractContext(DbContextOptions options) : DbContext(options)
{
    public abstract DbSet<Content> Contents { get; }
    public abstract DbSet<ContentPost> ContentPosts { get; }
    public abstract DbSet<ContentPostCollection> ContentPostCollections { get; }
    public abstract DbSet<ContentCollection> ContentCollections { get; }
    public abstract DbSet<Tag> Tags { get; }
}
