using ContentManager.Api.Contracts.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentManager.Api.Persistence.Data.Configuration;

public class ContentPostConfiguration : IEntityTypeConfiguration<ContentPost> {
    public void Configure(EntityTypeBuilder<ContentPost> builder) {

        builder
            .HasMany(p => p.Attachments)
            .WithOne(c => c.ContentPost)
            .HasForeignKey(c => c.PostId);
    }
}