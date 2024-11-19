using EventUnion.Api.Features.Common;
using EventUnion.CommonResources.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Addresses;

public static class GetCities
{
    [HttpGet("api/cities")]
    [AllowAnonymous]
    public class Endpoint : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            var state = Query<string?>("state");
            if (state is null)
                await SendOkAsync(StandardResponse.FromBadRequest("Estado não encontrado."), ct);
            
            var response = new Response
            {
                Collection = await IbgeUtilities.GetCitiesByState(state!)
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }

    private record Response
    {
        public List<string> Collection { get; set; } = [];
    }
}