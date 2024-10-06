using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Domain.Enum;
using ContentManager.Api.Contracts.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ContentManager.Api.Persistence.Data;

internal class ApplicationContext(DbContextOptions<ApplicationContext> options)
: ApplicationAbstractContext(options) {
    public override DbSet<Content> Contents => Set<Content>();
    public override DbSet<ContentPost> ContentPosts => Set<ContentPost>();
    public override DbSet<ContentPostCollection> ContentPostCollections => Set<ContentPostCollection>();
    public override DbSet<ContentCollection> ContentCollections => Set<ContentCollection>();
    public override DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresEnum<ContentType>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return base.SaveChangesAsync(cancellationToken);
    }
}
