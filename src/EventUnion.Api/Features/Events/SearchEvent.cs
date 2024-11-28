using Dapper;
using EventUnion.CommonResources.Response;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace EventUnion.Api.Features.Events;

// ReSharper disable once UnusedType.Global
public static class SearchEvent
{
    [HttpGet("api/events")]
    [AllowAnonymous]
    // ReSharper disable once UnusedType.Global
    public class Endpoint(IDbConnectionFactory dbConnectionFactory) : EndpointWithoutRequest
    {
        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = Query<string>("query");
    
            // Dividir a query em palavras e filtrar palavras com mais de 2 letras
            var words = query
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 2)
                .ToList();

            if (!words.Any())
            {
                // Se não houver palavras válidas, retornar uma resposta vazia ou um erro
                await SendOkAsync(StandardResponse.FromSuccess(new Response { Collection = [], Index = 0 }), ct);
                return;
            }

            using var connection = dbConnectionFactory.CreateOpenConnection();
    
            // Criar uma lista de parâmetros para a consulta
            var conditions = new List<string>();
            var parameters = new DynamicParameters();
    
            for (int i = 0; i < words.Count; i++)
            {
                var paramName = $"param{i}";
                conditions.Add($"e.name LIKE @{paramName} OR e.description LIKE @{paramName}");
                parameters.Add(paramName, "%" + words[i] + "%");
            }

            var sql = $@"
                    SELECT
                        e.event_id AS EventId,
                        e.image AS Image,
                        e.name AS Name,
                        e.start_date AS StartDate,
                        e.end_date AS EndDate,
                        e.private AS Private
                    FROM event e
                    WHERE {string.Join(" OR ", conditions)}";
    
            var events = await connection
                .QueryAsync<Response.Event>(sql, parameters);

            var list = events.ToList();
            var response = new Response
            {
                Collection = list.ToList(),
                Index = list.Count
            };

            await SendOkAsync(StandardResponse.FromSuccess(response), ct);
        }
    }
    
    public record Response
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public required List<Event> Collection { get; set; } = [];
        public required int Index { get; set; }
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