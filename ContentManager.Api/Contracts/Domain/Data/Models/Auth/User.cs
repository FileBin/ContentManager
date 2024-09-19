using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Contracts.Domain.Data.Models.Auth;

[Table("users")]
[PrimaryKey(nameof(Id))]
public class User {
    
    [MaxLength(DefaultConstraints.MaxUserIdLength)]
    public required string Id { get; set; }

    
    public required Guid GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    public virtual UserGroup Group { get; set; } = null!;
}