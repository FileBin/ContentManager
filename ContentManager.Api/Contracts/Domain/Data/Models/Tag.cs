using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("tags")]
[PrimaryKey(nameof(Name))]
public class Tag {
    public virtual IEnumerable<ContentPost> ContentPosts { get; set; } = null!;

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("parent_id")]
    public string? ParentName { get; set; }

    [ForeignKey(nameof(ParentName))]
    public virtual Tag? Parent { get; set; } = null!;
}
