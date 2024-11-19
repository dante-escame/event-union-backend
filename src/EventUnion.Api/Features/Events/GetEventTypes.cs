using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetTargetTypes
{
    [HttpGet("api/event-types")]
    [AllowAnonymous]
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            using var connection = dbConnectionFactory.CreateOpenConnection();

            const string sql = 
                """
                    SELECT
                        et.name AS Name 
                    FROM event_type et
                """;
            
            var eventTypes = await connection.QueryAsync<Response.EventType>(sql, ct);

            var response = new Response
            {
                Collection = eventTypes.Select(x => x.Name).ToList()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public List<string> Collection { get; set; } = [];
        public record EventType
        {
            public required string Name { get; init; }
        }
    }
}