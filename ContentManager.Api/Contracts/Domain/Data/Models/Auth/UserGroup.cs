using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Models.Auth;

[Table("user_groups")]
public class UserGroup : IAuthorizedResource {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(DefaultConstraints.MaxNameLength)]
    public required string Name { get; set; }

    public virtual IEnumerable<User> Users { get; set; } = null!;

    [Column("reader_group_id")]
    public Guid? ReaderGroupId { get; set; }

    [ForeignKey(nameof(ReaderGroupId))]
    public UserGroup? ReaderGroup { get; set; } = null!;

    [Column("editor_group_id")]
    public Guid? EditorGroupId { get; set; }

    [ForeignKey(nameof(EditorGroupId))]
    public UserGroup? EditorGroup { get; set; } = null!;

    [Column("owner_group_id")]
    public Guid? OwnerGroupId { get; set; }

    [ForeignKey(nameof(OwnerGroupId))]
    public UserGroup? OwnerGroup { get; set; } = null!;

    [Column("owner_user_id")]
    [MaxLength(DefaultConstraints.MaxUserIdLength)]
    public required string OwnerUserId { get; set; }

    [ForeignKey(nameof(OwnerUserId))]
    public User Owner { get; set; } = null!;

    public bool IsPublic { get; set; }
}