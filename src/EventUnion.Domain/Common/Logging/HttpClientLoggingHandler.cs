using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace EventUnion.CommonResources.Logging;

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public class HttpClientLoggingHandler(ILogger<HttpClientLoggingHandler> logger) 
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogInformation("{Method} {RequestUri}", request.Method, request.RequestUri);

        if (request.Content is not null)
            logger.LogInformation("{Request}", await request.Content.ReadAsStringAsync(cancellationToken));

        var response = await base.SendAsync(request, cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        logger.LogInformation("{ResponseContent}", responseContent);
        
        return response;
    }
}