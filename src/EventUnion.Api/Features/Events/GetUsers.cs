using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetUsers
{
    [HttpGet("api/users")]
    [AllowAnonymous]
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            using var connection = dbConnectionFactory.CreateOpenConnection();

            const string sql = 
                """
                    SELECT
                        u.user_id AS UserId, 
                        u.name AS Name
                    FROM User u
                """;
            
            var users = await connection.QueryAsync<Response.User>(sql, ct);

            var response = new Response
            {
                Users = users.ToList()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public List<User> Users { get; set; } = [];
        public record User
        {
            public Guid TagId { get; init; }
            public string? Name { get; init; }
        }
    }
}