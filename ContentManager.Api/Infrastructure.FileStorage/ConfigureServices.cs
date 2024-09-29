using ContentManager.Api.Contracts.Infrastructure.FileStorage.Services;
using ContentManager.Api.Infrastructure.FileStorage.Options;
using ContentManager.Api.Infrastructure.FileStorage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Api.Infrastructure.FileStorage;

public static class ConfigureServices {
    public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration config) {
        services.AddOptions(config);

        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IImageProcessingService, ImageProcessingService>();

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config) {
        services.AddOptions<FileStorageSettings>()
            .Bind(config.GetSection(FileStorageSettings.Key));
        return services;
    }
}