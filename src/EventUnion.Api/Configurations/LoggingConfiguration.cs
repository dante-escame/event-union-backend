using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using EventUnion.CommonResources.Logging;

namespace EventUnion.Api.Configurations;

public static class LoggingConfiguration
{
    public static void AddLoggingService(this IHostBuilder host)
    {
        host.UseSerilog((context, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration));
    }

    public static void AddRequestLoggingService(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.Request;
            logging.CombineLogs = true;
        });
    }

    public static void ConfigureAppLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestLogContextMiddleware>();

        app.UseHttpLogging();

        app.UseSerilogRequestLogging(cfg =>
        {
            cfg.IncludeQueryInRequestPath = true;
        });
    }
}