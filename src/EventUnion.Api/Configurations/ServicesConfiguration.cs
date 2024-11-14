using EventUnion.Api.Features.Common;
using FastEndpoints;
using FastEndpoints.Security;

namespace EventUnion.Api.Configurations;

public static class ServicesConfiguration
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
    
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddHttpEndpoints(this IServiceCollection services)
    {
        services.AddAuthenticationJwtBearer(s => s.SigningKey = Random256BytesKey.Value);
        
        services.AddAuthorization();
        
        services.AddFastEndpoints();

        return services;
    }
}