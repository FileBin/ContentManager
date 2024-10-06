using ContentManager.Api.Contracts.Domain.Enum;
using ContentManager.Api.Contracts.Persistance.Data;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using ContentManager.Api.Persistence.Repository;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ContentManager.Api.Persistence;

public static class ConfigureServices {
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration) {
        services.RegisterServices();

        var dataSource = configuration.GetDataSource();

        services.AddDbContext<ApplicationContext>(options
            => options.ConfigureOptions(dataSource));

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IDbContextAccessor, DbContextAccessor>();

        services.AddScoped<IEntityAccessor, EntityAccessor>();
        services.AddScoped<IEntityObtainer, EntityObtainer>();

        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentPostRepository, ContentPostRepository>();
        services.AddScoped<IContentCollectionRepository, ContentCollectionRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    internal static NpgsqlDataSource GetDataSource(this IConfiguration configuration) {
        var dataSourceBuilder =
            new NpgsqlDataSourceBuilder(configuration.GetConnectionString("ApplicationDatabase"));

        dataSourceBuilder.MapEnum<ContentType>();
        dataSourceBuilder.EnableParameterLogging();

        return dataSourceBuilder.Build();
    }

    internal static void ConfigureOptions(this DbContextOptionsBuilder options, NpgsqlDataSource dataSource) {
        options.UseNpgsql(dataSource)
            .UseSnakeCaseNamingConvention()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
}
