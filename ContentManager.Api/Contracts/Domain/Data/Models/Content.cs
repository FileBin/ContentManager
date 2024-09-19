using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Domain.Enum;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("contents")]
public class Content : IEntity {

    public Guid Id { get; set; }
    public required ContentType ContentType { get; set; }

    [MaxLength(DefaultConstraints.MaxPathLength)]
    public required string LocalFilePath { get; set; }
    public required int PostOrder { get; set; }
    public required int PostVariant { get; set; }
    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public virtual ContentPost ContentPost { get; set; } = null!;
}
