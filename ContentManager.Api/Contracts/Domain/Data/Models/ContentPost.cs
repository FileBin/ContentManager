using System.ComponentModel.DataAnnotations.Schema;
using ContentManager.Api.Contracts.Domain.Data.Interfaces;
using ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;
using ContentManager.Api.Contracts.Domain.Data.Models.Auth;

namespace ContentManager.Api.Contracts.Domain.Data.Models;

[Table("content_posts")]
public class ContentPost : IPost, IAuthorizedResource {
    [Column("id")]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = "";

    public bool IsPublished { get; set; } = false;

    public bool CanUsersEditTags { get; set; } = true;

    public virtual IEnumerable<Tag> Tags { get; set; } = null!;
    public virtual IEnumerable<Content> Attachments { get; set; } = null!;
    public virtual IEnumerable<ContentPostCollection> ContentPostCollections { get; set; } = null!;


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
}
