using System.Text.Json.Serialization;
using ContentManager.Api.Contracts.Application.Services;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Presentation.Configuration;
using ContentManager.Api.Presentation.Helpers;
using ContentManager.Api.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContentManager.Api.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config) {
        services.AddControllers()
        .AddApplicationPart(typeof(ConfigureServices).Assembly)
        .AddJsonOptions(options => {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddHttpContextAccessor();
        services.RegisterServices();

        services.AddAuth(config);

        services.AddCors(options => {
            options.AddPolicy(name: "dev",
                policy => {
                    policy.SetIsOriginAllowed(host => true);
                });
        });

        return services;
    }

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();

        if (app.Environment.IsDevelopment()) {
            app.UseCors("dev");
        }
        
        app.UseAuth();
        app.MapControllers();
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services) {
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddScoped<ICancellationTokenObtainer, CancellationTokenObtainer>();
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config) {

        var authConfig = config.GetRequiredSection(AuthConfiguration.Key)
            .Get<AuthConfiguration>();

        var swaggerConfig = config.GetRequiredSection(SwaggerConfiguration.Key)
            .Get<SwaggerConfiguration>();
        ArgumentNullException.ThrowIfNull(authConfig);
        ArgumentNullException.ThrowIfNull(swaggerConfig);

        services.AddAuthentication(options => {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.Authority = authConfig.ValidAuthority;
                options.Audience = authConfig.ValidAudience;
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorization();

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = swaggerConfig.Title, Version = "v1" });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows {
                    AuthorizationCode = new OpenApiOAuthFlow {
                        AuthorizationUrl = new Uri($"{authConfig.ValidAuthority}/protocol/openid-connect/auth"),
                        TokenUrl = new Uri($"{authConfig.ValidAuthority}/protocol/openid-connect/token"),
                        Scopes = swaggerConfig.OAuth.Scopes
                            .Select(x => new KeyValuePair<string, string>(x, x))
                            .ToDictionary()
                    }
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }

    private static void UseAuth(this WebApplication app) {
        app.UseAuthentication();
        app.UseAuthorization();

        var swaggerConfig = app.Configuration.GetRequiredSection(SwaggerConfiguration.Key).Get<SwaggerConfiguration>();
        ArgumentNullException.ThrowIfNull(swaggerConfig);

        if (!app.Environment.IsProduction()) {
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{swaggerConfig.Title} V1");

                options.OAuthClientId(swaggerConfig.OAuth.ClientId);
                options.OAuthClientSecret(swaggerConfig.OAuth.ClientSecret);
                options.OAuthAppName(swaggerConfig.OAuth.AppName);
                options.OAuthUsePkce();
            });
        }
    }
}
