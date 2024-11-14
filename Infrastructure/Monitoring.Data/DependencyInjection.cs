using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio;
using Monitoring.App.Interfaces.Authentication;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Authentication;
using Monitoring.Data.Configurations;
using Monitoring.Data.Database;
using Monitoring.Data.Storages;
using Monitoring.App.Interfaces;

namespace Monitoring.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddMinio(configuration)
            .AddAuth(configuration)
            .AddDatabase(configuration);

        return services;
    }

    private static IServiceCollection AddMinio(this IServiceCollection services, ConfigurationManager configuration)
    {
        var minioConfiguration = new MinioConfiguration();
        configuration.Bind(MinioConfiguration.SectionName, minioConfiguration);
        services.Configure<MinioConfiguration>(configuration.GetSection(MinioConfiguration.SectionName));

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(minioConfiguration.Endpoint)
            .WithCredentials(minioConfiguration.Login, minioConfiguration.Password)
            .WithSSL(minioConfiguration.UseSsl)
            .Build());

        services.AddScoped<IImageStorage, ImageStorage>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(JwtConfiguration.SectionName, jwtConfiguration);
        services.Configure<JwtConfiguration>(configuration.GetSection(JwtConfiguration.SectionName));

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddScoped<IUserStorage, UserStorage>();

        services.AddAuthorization();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<MonitoringDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IBuildingStorage, BuildingStorage>();
        services.AddScoped<ISensorStorage, SensorStorage>();
        services.AddScoped<ISensorRecordStorage, SensorRecordStorage>();
        services.AddScoped<INotificationStorage, NotificationStorage>();

        return services;
    }
}