using EventUnion.Api.Features.Common;
using EventUnion.CommonResources.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Addresses;

public static class GetCountries
{
    [HttpGet("api/countries")]
    [AllowAnonymous]
    public class Endpoint : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            var response = new Response
            {
                Collection = IbgeUtilities.GetCountries()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }

    private record Response
    {
        public List<string> Collection { get; set; } = [];
    }
}