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

        services.AddDbContext<ApplicationContext>();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IDbContextAccessor, DbContextAccessor>();

        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentPostRepository, ContentPostRepository>();
        services.AddScoped<IContentCollectionRepository, ContentCollectionRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
