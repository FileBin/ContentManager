using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Security.Services.Guards;
using ContentManager.Api.Security.Services.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Api.Security;

public static class ConfigureServices {
    public static IServiceCollection AddSecurity(this IServiceCollection services) {
        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        
        services.AddScoped<ISecureEntityObtainer, SecureObtainer>();
        
        SecureObtainer.RegisterReadGuards(services);
        SecureObtainer.RegisterContainers(services);
        
        services.AddScoped<IEntityWriteGuard, ContentWriteGuard>();
        services.AddScoped<IEntityWriteGuard, AuthorizedResourceWriteGuard>();

        services.AddScoped<ISecureUnitOfWork, SecureUnitOfWork>();

        return services;
    }
}
