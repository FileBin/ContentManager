using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

public class BaseEntity : IEntity {
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}