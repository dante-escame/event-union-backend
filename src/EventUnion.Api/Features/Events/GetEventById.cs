using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetEventById
{
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("events/{EventId}");
            AllowAnonymous();
        }
        
        public override async Task HandleAsync(CancellationToken ct)
        {
            var eventId = Route<Guid>("EventId");
            
            using var connection = dbConnectionFactory.CreateOpenConnection();

            const string sql = 
                """
                    SELECT
                        e.user_owner_id AS UserOwnerId,
                        e.image AS Image,
                        e.name AS Name,
                        e.description AS Description,
                        e.start_date AS StartDate,
                        e.end_date AS EndDate,
                        CONCAT(a.name, ' ', a.neighborhood, ' ', a.number AS VARCHAR) AS Address,
                        et.name AS EventType,
                        e.target AS Target,
                        e.private AS Private,
                        ARRAY(SELECT t.name FROM tag t
                              JOIN event_tag et ON et.event_id = e.event_id) AS Tags,
                        ARRAY(SELECT u.name FROM user u
                              JOIN event_user eu ON eu.user_id = u.user_id) AS ParticipantNames
                    FROM event e
                    LEFT JOIN event_address ea ON ea.event_id = e.event_id
                    LEFT JOIN address a ON a.address_id = ea.address_id 
                    LEFT JOIN event_type et ON et.id = e.event_type_id
                    WHERE e.id = @Guid
                """;

            var eventData = await connection
                .QuerySingleOrDefaultAsync<Response>(sql, new { eventId });
            if (eventData is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(StandardResponse.FromSuccess(eventData), ct);
        }
    }
    
    public record Response
    {
        public required string UserOwnerId { get; init; }
        public required string Image { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }
        public required string Address { get; init; }
        public required string EventType { get; init; }
        public required string Target { get; init; }
        public required bool Private { get; init; }
        public required List<string> Tags { get; init; } = [];
        public required List<string> ParticipantNames { get; init; } = [];
    }
}