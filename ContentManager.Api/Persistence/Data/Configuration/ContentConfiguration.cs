using ContentManager.Api.Contracts.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentManager.Api.Persistence.Data.Configuration;

public class ContentConfiguration : IEntityTypeConfiguration<Content> {
    public void Configure(EntityTypeBuilder<Content> builder) {
        builder
            .Property(c => c.QualityLevels)
            .HasColumnType("jsonb");

        builder
            .HasOne(c => c.ContentPost)
            .WithMany(p => p.Attachments)
            .HasForeignKey(c => c.PostId);
    }
}
