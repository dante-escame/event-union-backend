using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetTags
{
    [HttpGet("api/tags")]
    [AllowAnonymous]
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            using var connection = dbConnectionFactory.CreateOpenConnection();

            const string sql = 
                """
                    SELECT
                        t.tag_id AS TagId, 
                        p.name AS Name
                    FROM tb_tags t
                """;
            
            var tags = await connection.QueryAsync<Response.Tag>(sql, ct);

            var response = new Response
            {
                Tags = tags.ToList()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public List<Tag> Tags { get; set; } = [];
        public record Tag
        {
            public Guid TagId { get; init; }
            public string? Name { get; init; }
        }
    }
}