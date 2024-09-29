using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Contracts.Domain.Enum;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("contents")]
[Index(nameof(PostId), nameof(PostOrder), nameof(PostVariant), IsUnique = true)]
public class Content : IEntity {

    public Guid Id { get; set; }
    public required ContentType ContentType { get; set; }

    [MaxLength(DefaultConstraints.MaxPathLength)]
    public required string LocalFilePath { get; set; }

    public required int PostOrder { get; set; }
    public int PostVariant { get; set; } = 1;

    public Dictionary<string, string>? QualityLevels { get; set; }

    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public virtual ContentPost ContentPost { get; set; } = null!;
}
