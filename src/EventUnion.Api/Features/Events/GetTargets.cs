using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetTargets
{
    [HttpGet("api/targets")]
    [AllowAnonymous]
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            using var connection = dbConnectionFactory.CreateOpenConnection();

            const string sql = 
                """
                    SELECT
                        t.name AS Name 
                    FROM Target t
                """;
            
            var targets = await connection.QueryAsync<Response.Target>(sql, ct);

            var response = new Response
            {
                Targets = targets.Select(x => x.Name).ToList()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public List<string> Targets { get; set; } = [];
        public record Target
        {
            public required string Name { get; init; }
        }
    }
}