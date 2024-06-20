using Carter;
using Event.Api.DTOs;
using Event.Api.Entities;
using Event.Api.Infrastructure;
using Event.Api.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Event.Api.Features.Users;

public static class DeleteUser
{
    public class Command(Guid userId) : IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }= userId;
    }

    internal sealed class Handler(EventDbContext dbContext)
        : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            var userEntity = await dbContext.User
                .Where(x => x.UserId == command.UserId)
                .Include(x => x.Interests)
                .Include(x => x.Person)
                .Include(x => x.Company)
                .Include(x => x.UserAddress)
                .ThenInclude(x => x.Address)
                .Include(x => x.Phone)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (userEntity is not null)
                dbContext.Remove(userEntity);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return command.UserId;
        }
    }
}

public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/users/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var command = new DeleteUser.Command(userId);

            var result = await sender.Send(command);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}