using CSharpFunctionalExtensions;
using Dapper;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using MediatR;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class GetEventById
{
    public record Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid EventId { get; set; }
    }
    
    public class Endpoint(ISender sender) : Endpoint<Request, Result<Response, Error>>
    {
        public override void Configure()
        {
            Get("api/events/{EventId}");
            AllowAnonymous();
        }
        
        public override async Task<Result<Response, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var querySave = new Query(req.EventId);
            
            return await sender.Send(querySave, ct);
        }
        
        #region Handler
        public record Query(Guid EventId) : IRequest<Result<Response, Error>>;
        
        internal class Handler(
            IDbConnectionFactory dbConnectionFactory)
            : IRequestHandler<Query, Result<Response, Error>>
        {
            public async Task<Result<Response, Error>> Handle(Query request, CancellationToken ct)
            {
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
                            CONCAT(a.street, ' ', a.neighborhood, ' ', a.number::varchar) AS Address,
                            et.name AS EventType,
                            e.name AS Target,
                            e.private AS Private,
                            ARRAY(SELECT DISTINCT t.name FROM tag t
                                JOIN event_tag et ON et.event_id = e.event_id) AS Tags,
                            ARRAY(SELECT DISTINCT u.email FROM "user" u
                                JOIN event_user uu ON uu.user_id = u.user_id) AS ParticipantNames
                        FROM event e
                             LEFT JOIN event_address ea ON ea.event_id = e.event_id
                             LEFT JOIN address a ON a.address_id = ea.address_id
                             LEFT JOIN event_type et ON et.event_type_id = e.event_type_id
                        WHERE e.event_id = @eventId
                    """;

                var response = await connection
                    .QuerySingleOrDefaultAsync<Response>(sql, new { request.EventId });
                if (response is null)
                    return CommonError.NotFound();

                return response;
            }
        }
        #endregion
    }
    
    public record Response
    {
        public required Guid UserOwnerId { get; init; }
        public required string Image { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }
        public required string Address { get; init; }
        public required string EventType { get; init; }
        public required string Target { get; init; }
        public required bool Private { get; init; }
        // ReSharper disable CollectionNeverUpdated.Global
        public required string[] Tags { get; init; } = [];
        public required string[] ParticipantNames { get; init; } = [];
        // ReSharper restore CollectionNeverUpdated.Global
    }
}