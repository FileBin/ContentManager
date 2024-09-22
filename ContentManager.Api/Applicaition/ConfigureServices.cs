using ContentManager.Api.Application.Services;
using ContentManager.Api.Contracts.Application.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Api.Application;

public static class ConfigureServices {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ConfigureServices).Assembly);

        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IContentPostService, ContentPostService>();
        services.AddScoped<IContentPostContentService, ContentPostContentService>();

        return services;
    }
}
