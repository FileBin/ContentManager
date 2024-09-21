namespace ContentManager.Api.Contracts.Domain;

public static class DefaultConstraints {
    public const int MaxNameLength = 128;
    public const int MaxDescriptionLength = 2048;
    public const int MaxPathLength = 256;
    public const int MaxUserIdLength = 36;
    public const int TagMaxDepth = 8;
}