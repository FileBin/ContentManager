using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Contracts.Domain.Data.Interfaces;
using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;
using ContentManager.Api.Contracts.Domain.Data.Models.Auth;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("content_posts")]
public class ContentPost : IPost, IAuthorizedResource {
    public Guid Id { get; set; }

    [MaxLength(DefaultConstraints.MaxNameLength)]
    public required string Name { get; set; }

    [MaxLength(DefaultConstraints.MaxDescriptionLength)]
    public string Description { get; set; } = "";
    public bool IsPublic { get; set; } = false;
    public bool IsDraft { get; set; } = false;
    public bool CanUsersEditTags { get; set; } = true;

    public Guid? ReaderGroupId { get; set; }
    [ForeignKey(nameof(ReaderGroupId))]
    public UserGroup? ReaderGroup { get; set; } = null!;

    public Guid? EditorGroupId { get; set; }
    [ForeignKey(nameof(EditorGroupId))]
    public UserGroup? EditorGroup { get; set; } = null!;

    public required Guid? OwnerGroupId { get; set; }
    [ForeignKey(nameof(OwnerGroupId))]
    public UserGroup? OwnerGroup { get; set; } = null!;
    
    [MaxLength(DefaultConstraints.MaxUserIdLength)]
    public required string OwnerUserId { get; set; }
    [ForeignKey(nameof(OwnerUserId))]
    public User Owner { get; set; } = null!;

    public virtual IEnumerable<Tag> Tags { get; set; } = null!;
    public virtual IEnumerable<Content> Attachments { get; set; } = null!;
    public virtual IEnumerable<ContentPostCollection> ContentPostCollections { get; set; } = null!;
}
