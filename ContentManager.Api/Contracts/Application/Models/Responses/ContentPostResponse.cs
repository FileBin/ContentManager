namespace ContentManager.Api.Contracts.Application.Models.Responses;

using ContentVariantResponse = Dictionary<int, Guid>;

public record ContentPostResponse {
    public required Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public string[] Tags { get; init; } = [];
    public ContentVariantResponse[] ContentVariants { get; init; } = [];

    public bool IsPublic { get; init; }
    public bool IsDraft { get; init; }
    public bool CanUsersEditTags { get; init; } = false;

    //FIXME SECURITY WRN: make this group ids null for readers and anonymous users
    public string? ReaderGroupName { get; init; }
    public string? EditorGroupName { get; init; }
    public string? OwnerGroupName { get; init; }
}