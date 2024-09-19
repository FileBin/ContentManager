using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Domain.Enum;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("contents")]
public class Content : IEntity {

    [Column("id")]
    public Guid Id { get; set; }

    [Column("content_type")]
    public required ContentType ContentType { get; set; }

    [Column("file_path")]
    [MaxLength(DefaultConstraints.MaxPathLength)]
    public required string LocalFilePath { get; set; }

    [Column("post_order")]
    public required int PostOrder { get; set; }

    [Column("post_variant")]
    public required int PostVariant { get; set; }

    [Column("post_id")]
    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public virtual ContentPost ContentPost { get; set; } = null!;
}
