using ContentManager.Api.Contracts.Domain.Data.Models;
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
        //TODO Refactor all this posts to be one base class or use DI
        services.AddScoped<ISecureContentRepository, SecureContentRepository>();
        services.AddScoped<ISecureContentPostRepository, SecureContentPostRepository>();
        services.AddScoped<ISecureContentCollectionRepository, SecureContentCollectionRepository>();

        services.AddScoped<IEntityReadGuard<Content>, ContentReadGuard>();
        services.AddScoped<IEntityReadGuard<ContentPost>, AuthorizedResourceReadGuard<ContentPost>>();
        services.AddScoped<IEntityReadGuard<ContentCollection>, AuthorizedResourceReadGuard<ContentCollection>>();
        
        services.AddScoped<IEntityWriteGuard, ContentWriteGuard>();
        services.AddScoped<IEntityWriteGuard, AuthorizedResourceWriteGuard>();

        services.AddScoped<ISecureUnitOfWork, SecureUnitOfWork>();

        return services;
    }
}
