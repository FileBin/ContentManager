using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Infrastructure.FileStorage.Options;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ContentManager.Api.Infrastructure.FileStorage.Services;

public class ImageProcessingService(IOptions<FileStorageSettings> fileStorageSettings) : IImageProcessingService {

    public async Task<Stream> GetPreviewAsync(Stream fileStream, string settingName) {
        using var image = await Image.LoadAsync(fileStream);
        var memoryStream = new MemoryStream();

        var setting = fileStorageSettings.Value.ImageSettings[settingName];

        var dimensions = setting.MaxSize
            .Trim().Split('x').Select(int.Parse).ToArray();

        var resizeOptions = new ResizeOptions {
            Size = new Size(dimensions[0], dimensions[1]),
            Sampler = KnownResamplers.Lanczos2,
            Compand = true,
            Mode = ResizeMode.Min
        };

        image.Mutate(img => img.Resize(resizeOptions));

        var encoder = new JpegEncoder() {
            Quality = 30
        };

        await image.SaveAsync(memoryStream, encoder);
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        return memoryStream;
    }
}
