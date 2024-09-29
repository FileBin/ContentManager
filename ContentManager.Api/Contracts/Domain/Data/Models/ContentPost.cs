using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Contracts.Domain.Data.Interfaces;
using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

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

    [MaxLength(DefaultConstraints.MaxUserGroupLength)]
    public string? ReaderGroupName { get; set; }

    [MaxLength(DefaultConstraints.MaxUserGroupLength)]
    public string? EditorGroupName { get; set; }
    
    [MaxLength(DefaultConstraints.MaxUserGroupLength)]
    public required string? OwnerGroupName { get; set; }
    
    [MaxLength(DefaultConstraints.MaxUserIdLength)]
    public required string OwnerUserId { get; set; }

    public virtual ISet<Tag> Tags { get; set; } = null!;
    public virtual ICollection<Content> Attachments { get; set; } = null!;
    public virtual ISet<ContentPostCollection> ContentPostCollections { get; set; } = null!;
}
