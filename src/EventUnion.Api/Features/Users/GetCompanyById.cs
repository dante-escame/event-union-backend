using CSharpFunctionalExtensions;
using Dapper;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;
using EventUnion.Domain.Common.Interfaces;
using FastEndpoints;
using MediatR;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Users;

public static class GetCompanyById
{
    public record Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid UserId { get; set; }
    }
    
    // ReSharper disable once UnusedMember.Global
    public class Endpoint(ISender sender) : Endpoint<Request, Result<Response, Error>>
    {
        public override void Configure()
        {
            Get("api/companies/{UserId}");
            AllowAnonymous();
        }

        public override async Task<Result<Response, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var querySave = new Query(req.UserId);
            return await sender.Send(querySave, ct);
        }
        
        #region Handler
        public record Query(Guid UserId) : IRequest<Result<Response, Error>>;

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
                            c.legal_name AS LegalName,
                            c.trade_name AS TradeName,
                            u.email AS Email,
                            u.password AS Password,
                            h.value AS PhoneNumber,
                            CONCAT(a.street, ' ', a.neighborhood, ' ', a.number::varchar) AS Address,
                            c.specialization AS Specialization,
                            ARRAY(SELECT DISTINCT t.name FROM tag t
                                    JOIN user_tag ut ON ut.tag_id = t.tag_id AND ut.user_id = u.user_id) AS Tags
                        FROM company c
                             LEFT JOIN "user" u ON u.user_id = c.user_id
                             LEFT JOIN user_address ua ON ua.user_address_id = u.user_id
                             LEFT JOIN address a ON a.address_id = ua.address_id
                             LEFT JOIN phone h ON h.user_id = u.user_id
                        WHERE u.user_id = @userId
                    """;

                var response = await connection
                    .QuerySingleOrDefaultAsync<Response>(sql, new { request.UserId });
                if (response is null)
                    return CommonError.NotFound();

                return response;
            }
        }
        #endregion
    }

    public record Response
    {
        public required string LegalName { get; init; }
        public required string TradeName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Address { get; init; }
        public required string Specialization { get; init; }
        public required string[] Tags { get; init; } = [];
    }
}