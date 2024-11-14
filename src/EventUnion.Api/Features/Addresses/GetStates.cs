using EventUnion.Api.Features.Common;
using EventUnion.CommonResources.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Addresses;

public static class GetStates
{
    [HttpGet("api/states")]
    [AllowAnonymous]
    public class Endpoint : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            var response = new Response
            {
                States = IbgeUtilities.GetStates()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }

    private record Response
    {
        public List<string> States { get; set; } = [];
    }
}