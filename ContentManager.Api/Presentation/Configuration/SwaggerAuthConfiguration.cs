namespace ContentManager.Api.Presentation.Configuration;

internal record SwaggerConfiguration {
    public const string Key = "Swagger";

    public string Title { get; init; } = "api";
    public required OAuthConfiguration OAuth { get; init; }
}

internal record OAuthConfiguration {
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string AppName { get; init; } = "Swagger UI";
    public required string[] Scopes {get; init; } = [];
}
