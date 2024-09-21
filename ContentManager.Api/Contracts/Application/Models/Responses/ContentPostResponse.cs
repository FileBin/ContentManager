namespace ContentManager.Api.Contracts.Application.Models.Responses;

public record ContentPostResponse {
    public string Name { get; init; } = "";

    public string Description { get; init; } = "";
    public string[] Tags { get; init; } = [];

    public bool CanUsersEditTags { get; init; } = false;

    //FIXME SECURITY WRN: make this group ids null for readers and anonymous users
    public Guid? ReaderGroupId { get; init; }
    public Guid? EditorGroupId { get; init; }
    public Guid? OwnerGroupId { get; init; }
}