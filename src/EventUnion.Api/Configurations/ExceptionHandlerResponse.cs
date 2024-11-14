using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using EventUnion.CommonResources;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Api.Configurations;

public static class ExceptionHandlerResponse
{
    public static void ConfigureGlobalExceptionHandler(this WebApplication app)
    {
        var isDevelopmentEnvironment = EnvironmentDefaults.IsDevelopmentEnvironment(app.Environment);
        if (isDevelopmentEnvironment)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(ExceptionHandlerForDevelopmentEnvironment);
            });
        }
        else
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(ExceptionHandlerForProductionEnvironment);
            });
        }
    }

    private static async Task ExceptionHandlerForDevelopmentEnvironment(HttpContext ctx)
    {
        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

        var error = exHandlerFeature is null
            ? CommonError.InternalServerError()
            : CommonError.InternalServerError(exHandlerFeature.Error);

        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ctx.Response.ContentType = "application/problem+json";

        await ctx.Response.WriteAsJsonAsync(StandardResponse.FromInternalServerError(string.Empty));
    }
    
    private static async Task ExceptionHandlerForProductionEnvironment(HttpContext ctx)
    {
        var error = CommonError.InternalServerError();

        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ctx.Response.ContentType = "application/problem+json";

        await ctx.Response.WriteAsJsonAsync(StandardResponse.FromInternalServerError(string.Empty));
    }
}