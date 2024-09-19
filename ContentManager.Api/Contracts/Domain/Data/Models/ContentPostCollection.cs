using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("content_posts_collections")]
[PrimaryKey(nameof(PostId), nameof(CollectionId))]
public class ContentPostCollection {
    public int CollectionOrder { get; set; }
    
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public virtual ContentPost Post { get; set; } = null!;

    public Guid CollectionId { get; set; }
    [ForeignKey(nameof(CollectionId))]
    public virtual ContentCollection Collection { get; set; } = null!;

}
