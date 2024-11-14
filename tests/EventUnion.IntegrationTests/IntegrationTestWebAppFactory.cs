using EventUnion.Api;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventUnion.Infrastructure;
using Testcontainers.PostgreSql;

namespace EventUnion.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("db_application")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentDefaults.IntegrationTestEnvironment);

        builder.ConfigureTestServices(services =>
        {
            ConfigureDbWrite(services);

            ConfigureDbRead(services);

            services.AddMassTransitTestHarness();
        });

        // TODO: find a better solution to:
        //System.ArgumentException :
        //Cannot write DateTime with Kind = Local to PostgreSQL type 'timestamp with time zone',
        //only UTC is supported.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private void ConfigureDbWrite(IServiceCollection services)
    {
        var descriptor = services
            .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<EventUnionDbContext>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<EventUnionDbContext>(options =>
        {
            options
                .UseNpgsql(_dbContainer.GetConnectionString());
        });
    }

    private void ConfigureDbRead(IServiceCollection services)
    {
        var descriptor = services
            .SingleOrDefault(s => s.ServiceType == typeof(DbConnectionFactory));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.Configure<ConnectionStringOptions>(x => x.ConnectionString = _dbContainer.GetConnectionString());

        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}