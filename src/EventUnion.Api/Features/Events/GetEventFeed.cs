using CSharpFunctionalExtensions;
using Dapper;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using MediatR;

namespace EventUnion.Api.Features.Events;

public static class GetEventFeed
{
    public record Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid UserId { get; set; }
    }
    
    // ReSharper disable once UnusedType.Global
    public class Endpoint(ISender sender) : Endpoint<Request, Result<Response, Error>>
    {
        public override void Configure()
        {
            Get("api/events/users/UserId");
            AllowAnonymous();
        }
        
        public override async Task<Result<Response, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var querySave = new Query(req.UserId);
            
            return await sender.Send(querySave, ct);
        }
        
        #region Handler
        public record Query(Guid UserId) : IRequest<Result<Response, Error>>;
        
        // ReSharper disable once UnusedType.Global
        internal class Handler(
            IDbConnectionFactory dbConnectionFactory)
            : IRequestHandler<Query, Result<Response, Error>>
        {
            public async Task<Result<Response, Error>> Handle(Query request, CancellationToken ct)
            {
                using var connection = dbConnectionFactory.CreateOpenConnection();
                
                /*const string recommendedEventSql = 
                    """
                        SELECT
                            e.event_id AS EventId,
                            e.image AS Image,
                            e.name AS Name,
                            e.start_date AS StartDate,
                            e.end_date AS EndDate,
                            e.private AS Private
                        FROM event e
                            JOIN "event_user" eu on eu.event_id = e.event_id
                            JOIN "user" u on u.user_id = eu.user_id
                        WHERE u.user_id = @userId
                    """;*/
                
                const string nextEventsSql = 
                    """
                        SELECT
                            e.event_id AS EventId,
                            e.image AS Image,
                            e.name AS Name,
                            e.start_date AS StartDate,
                            e.end_date AS EndDate,
                            e.private AS Private
                        FROM event e
                            JOIN "event_user" eu on eu.event_id = e.event_id
                            JOIN "user" u on u.user_id = eu.user_id
                        WHERE u.user_id = @userId
                    """;

                var nextEvents = await connection
                    .QueryAsync<Response.Event>(nextEventsSql, new { request.UserId });
                
                var response = new Response
                {
                    Collection = nextEvents.ToList()
                };

                return response;
            }
        }
        #endregion
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