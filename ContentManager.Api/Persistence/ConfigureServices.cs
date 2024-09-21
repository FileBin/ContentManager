using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using ContentManager.Api.Persistence.Repository;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Api.Persistence;

public static class ConfigureServices {
    public static IServiceCollection AddPersistance(this IServiceCollection services) {
        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddScoped<IDbContextAccessor, DbContextAccessor>();

        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentPostRepository, ContentPostRepository>();
        services.AddScoped<IContentCollectionRepository, ContentCollectionRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserGroupRepository, UserGroupRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
