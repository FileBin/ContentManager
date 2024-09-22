namespace ContentManager.Api.Infrastructure.FileStorage.Options;

public class FileStorageSettings
{
    public const string Key = "FileStorage";
    public string RootFolder { get; set; } = "file_storage";
}
