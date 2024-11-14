using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using EventUnion.Api.Configurations;
using EventUnion.Infrastructure;
using EventUnion.CommonResources.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequestLoggingService();

builder.Host.AddLoggingService();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(typeof(Program).Assembly);

    x.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
});

builder.Services.AddServices();

builder.Services.AddPersistence(builder.Configuration, builder.Environment);

builder.Services.ConfigureMessagingServices(builder.Configuration);

builder.Services.AddHttpEndpoints();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.ApplyMigrations();

app.ConfigureApplication();

app.ConfigureAppLogging();

app.MapHealthChecks("/event-union/health").AllowAnonymous();

app.MapHealthChecks("/event-union/health/status", new HealthCheckOptions
{
    ResponseWriter = async (context, _) =>
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var informationalVersion = executingAssembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;
        var product = executingAssembly
            .GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        var result = $"{product} - {informationalVersion}";

        context.Response.ContentType = MediaTypeNames.Text.Plain;
        await context.Response.WriteAsync(result);
    }
}).AllowAnonymous();

app.Run();

namespace EventUnion.Api
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program;
}