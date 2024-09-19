using System.ComponentModel.DataAnnotations;
using ContentManager.Api.Contracts.Domain;

namespace ContentManager.Api.Contracts.Application.Models.Requests;

public record CreatePostRequest {
    [MaxLength(DefaultConstraints.MaxNameLength)]
    public required string Name { get; init; }

    [MaxLength(DefaultConstraints.MaxDescriptionLength)]
    public string? Description { get; init; }
    public string[] Tags { get; init; } = [];

    public bool? CanUsersEditTags { get; init; }
    public Guid? ReaderGroupId { get; init; }
    public Guid? EditorGroupId { get; init; }
    public required Guid OwnerGroupId { get; init; }
}

public record UpdatePostRequest {
    [MaxLength(DefaultConstraints.MaxNameLength)]
    public string? Name { get; init; }

    [MaxLength(DefaultConstraints.MaxDescriptionLength)]
    public string? Description { get; init; }
    public bool? CanUsersEditTags { get; init; }
    public string[]? Tags { get; init; }
}