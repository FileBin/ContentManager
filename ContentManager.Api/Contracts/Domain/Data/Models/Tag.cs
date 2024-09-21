using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("tags")]
[PrimaryKey(nameof(Name))]
public class Tag {
    [MaxLength(DefaultConstraints.MaxNameLength)]
    public required string Name { get; set; }

    [MaxLength(DefaultConstraints.MaxDescriptionLength)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(DefaultConstraints.MaxNameLength)]
    public string? ParentName { get; set; }

    [ForeignKey(nameof(ParentName))]
    public virtual Tag? Parent { get; set; } = null!;

    public virtual ICollection<ContentPost> ContentPosts { get; set; } = null!;
}
