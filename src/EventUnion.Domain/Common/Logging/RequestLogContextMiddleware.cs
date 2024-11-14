using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace EventUnion.CommonResources.Logging;

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public class RequestLogContextMiddleware(RequestDelegate next)
{
    // ReSharper disable once UnusedMember.Global
    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty(LoggingDefaults.CorrelationLogProperty, context.TraceIdentifier))
        {
            return next(context);
        }
    }
}