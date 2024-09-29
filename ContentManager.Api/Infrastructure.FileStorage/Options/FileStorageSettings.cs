namespace ContentManager.Api.Infrastructure.FileStorage.Options;

public record FileStorageSettings {
    public const string Key = "FileStorage";
    public string RootFolder { get; init; } = "file_storage";
    public Dictionary<string, ImageSettings> ImageSettings { get; set; } = [];
}

public record ImageSettings {
    public required string MaxSize { get; init; }
}
