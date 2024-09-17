using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("content_posts_collections")]
[PrimaryKey(nameof(PostId), nameof(CollectionId))]
public class ContentPostCollection {
    [Column("post_id")]
    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public virtual ContentPost Post { get; set; } = null!;

    [Column("collection_id")]
    public Guid CollectionId { get; set; }

    [ForeignKey(nameof(CollectionId))]
    public virtual ContentCollection Collection { get; set; } = null!;

    [Column("collection_order")]
    public int CollectionOrder { get; set; }
}
