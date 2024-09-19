using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Persistance.Data;

public interface IApplicationContext
{
    public DbSet<Content> Contents { get; }
    public DbSet<ContentPost> ContentPosts { get; }
    public DbSet<ContentPostCollection> ContentPostCollections { get; }
    public DbSet<ContentCollection> ContentCollections { get; }
    public DbSet<Tag> Tags { get; }
    public DbSet<User> Users { get; }
    public DbSet<UserGroup> UserGroups { get; }
}
