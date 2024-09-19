using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using ContentManager.Api.Contracts.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Data;

internal class ApplicationContext : DbContext, IApplicationContext {
    public DbSet<Content> Contents => Set<Content>();
    public DbSet<ContentPost> ContentPosts => Set<ContentPost>();
    public DbSet<ContentPostCollection> ContentPostCollections => Set<ContentPostCollection>();
    public DbSet<ContentCollection> ContentCollections => Set<ContentCollection>();
    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<User> Users => Set<User>();
    public DbSet<UserGroup> UserGroups => Set<UserGroup>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder
            .UseNpgsql("<connection string>")
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return base.SaveChangesAsync(cancellationToken);
    }
}
