using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using EventUnion.CommonResources;
using EventUnion.CommonResources.Response;
using EventUnion.Infrastructure;

namespace EventUnion.Api.Configurations;

public static class AppConfiguration
{
    public static void ConfigureApplication(this WebApplication app)
    {
        app.ConfigureEndpoints();
        app.ConfigureGlobalExceptionHandler();
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        var isValidEnvironment = 
            EnvironmentDefaults.IsDevelopmentEnvironment(app.Environment) 
            || app.Environment.IsEnvironment(EnvironmentDefaults.IntegrationTestEnvironment);
        
        if (!isValidEnvironment) return;

        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<EventUnionDbContext>();

        if (app.Environment.IsEnvironment(EnvironmentDefaults.IntegrationTestEnvironment))
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
        }
    }

    private static void ConfigureEndpoints(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseFastEndpoints(c =>
        {
            c.Endpoints.Configurator = ep =>
            {
                ep.DontAutoSendResponse();
                ep.PostProcessor<StandardResponseSenderProcessor<EmptyUser>>(Order.After);
            };

            c.Errors.ResponseBuilder = StandardResponseErrorBuilder.BuildResponse;
        });
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class EmptyUser;