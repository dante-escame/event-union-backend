using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Interfaces;

namespace EventUnion.Infrastructure;

public static class Configuration
{
    public static void AddPersistence(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment builderEnvironment)
    {
        var connectionString = configuration.GetConnectionString("EventUnionDbContext");
        var isDevelopment = EnvironmentDefaults.IsDevelopmentEnvironment(builderEnvironment);

        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.ConfigureDbWrite(connectionString, isDevelopment);

        services.ConfigureDbRead(connectionString);

        services.AddRepositories();
    }

    private static void ConfigureDbWrite(this IServiceCollection services, string connectionString,
        bool isDevelopment)
    {
        services.AddDbContext<EventUnionDbContext>((_, options) =>
        {
            options.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(Assembly.GetAssembly(typeof(EventUnionDbContext))?.FullName);
            })
            .UseSnakeCaseNamingConvention();

            if (isDevelopment)
            {
                options
                    .UseLoggerFactory(CreateLoggerFactory())
                    .EnableSensitiveDataLogging();
            }
            else
            {
                options
                    .UseLoggerFactory(CreateEmptyLoggerFactory());
            }
        });

        // TODO: find a better solution to Npgsql.EnableLegacyTimestampBehavior
        // System.ArgumentException :
        // Cannot write DateTime with Kind = Local to PostgreSQL type 'timestamp with time zone',
        // only UTC is supported.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        // TODO repositories
    }

    private static void ConfigureDbRead(this IServiceCollection services,
        string connectionString)
    {
        services.Configure<ConnectionStringOptions>(x => x.ConnectionString = connectionString);

        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
    }

    private static ILoggerFactory CreateEmptyLoggerFactory()
    {
        return LoggerFactory.Create(builder => builder
            .AddFilter((_, _) => false));
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => builder
            .AddFilter((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddConsole());
    }
}

public class ConnectionStringOptions
{
    public string? ConnectionString { get; set; }
}