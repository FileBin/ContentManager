namespace ContentManager.Api.Presentation.Configuration;

internal record AuthConfiguration {
    public const string Key = "Auth";

    public required string ValidAudience { get; init; }
    public required string ValidAuthority { get; init; }
}
