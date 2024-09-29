namespace ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;

public interface IFileStorageService {
    StreamReader ReadFile(string filename);
    StreamWriter WriteFile(string filename);
}
