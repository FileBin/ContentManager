using System.ComponentModel.DataAnnotations;
using ContentManager.Api.Contracts.Domain;

namespace ContentManager.Api.Contracts.Application.Models.Requests;

public record ContentPostUpdateRequest {
    [MaxLength(DefaultConstraints.MaxNameLength)]
    public string? Name { get; init; }

    [MaxLength(DefaultConstraints.MaxDescriptionLength)]
    public string? Description { get; init; }
    public bool? CanUsersEditTags { get; init; }
    public string[]? Tags { get; init; }

    public string? ReaderGroupName { get; init; }
    public string? EditorGroupName { get; init; }
    public string? OwnerGroupName { get; init; }
}