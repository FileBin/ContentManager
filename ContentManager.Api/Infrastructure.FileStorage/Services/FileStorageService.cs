using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Infrastructure.FileStorage.Options;
using Microsoft.Extensions.Options;

namespace ContentManager.Api.Infrastructure.FileStorage.Services;

public class FileStorageService(IOptions<FileStorageSettings> fileStorageSettings) : IFileStorageService {

    public StreamReader ReadFile(string filename) {
        var filepath = Path.Combine(fileStorageSettings.Value.RootFolder, filename);

        return new StreamReader(filepath);
    }

    public StreamWriter WriteFile(string filename) {
        var filepath = Path.Combine(fileStorageSettings.Value.RootFolder, filename);

        var directories = Path.GetDirectoryName(filepath);
        if (directories != null) {
            Directory.CreateDirectory(directories);
        }
        
        return new StreamWriter(filepath);
    }
}
