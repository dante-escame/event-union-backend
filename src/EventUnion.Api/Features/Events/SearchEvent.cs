using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;

namespace EventUnion.Api.Features.Events;

// ReSharper disable once UnusedType.Global
public static class SearchEvent
{
    [HttpGet("api/events")]
    // ReSharper disable once UnusedType.Global
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = Query<string>("query");
            
            using var connection = dbConnectionFactory.CreateOpenConnection();
            
            const string sql = 
                """
                    SELECT
                        e.event_id AS EventId,
                        e.image AS Image,
                        e.name AS Name,
                        e.start_date AS StartDate,
                        e.end_date AS EndDate,
                        e.private AS Private
                    FROM event e
                    WHERE e.name like @param
                        or e.description like @param
                """;
            
            var events = await connection
                .QueryAsync<Response.Event>(sql, new { param = "%" + query + "%" });

            var response = new Response
            {
                Collection = events.ToList()
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public required List<Event> Collection { get; set; } = [];
        public record Event
        {
            public required Guid EventId { get; init; }
            public required string Image { get; init; }
            public required string Name { get; init; }
            public required DateTime StartDate { get; init; }
            public required DateTime EndDate { get; init; }
            public required bool Private { get; init; }
        }
    }
}