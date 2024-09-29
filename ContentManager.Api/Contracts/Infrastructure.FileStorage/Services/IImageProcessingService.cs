namespace ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;

public interface IImageProcessingService {
    Task<Stream> GetPreviewAsync(Stream fileStream, string settingName);
}