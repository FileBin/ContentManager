using System.ComponentModel.DataAnnotations.Schema;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Models.Auth;

[Table("user_groups")]
public class UserGroup : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    public virtual IEnumerable<User> Users { get; set; } = null!;
}