using ContentManager.Api.Contracts.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ContentConfiguration : IEntityTypeConfiguration<Content> {
    public void Configure(EntityTypeBuilder<Content> builder) {
        builder
            .Property(p => p.QualityLevels)
            .HasColumnType("jsonb");
    }
}