using Carter;
using Event.Api.DTOs;
using Event.Api.Infrastructure;
using Event.Api.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace Event.Api.Features.Events;

public static class CreateEvent
{
    public class Command : IRequest<Result<Guid>>
    {
        // TODO command attributes
        public Guid EventId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            // TODO validation rules ("rule for")
        }
    }

    internal sealed class Handler(EventDbContext dbContext, IValidator<Command> validator)
        : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error("CreateEvent.Validation",
                    validationResult.ToString()));
            }

            Entities.Event? eventEntity = default;
            
            await dbContext.AddAsync(eventEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return eventEntity.EventId;
        }
    }
}

public class CreateEventEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/events", async (CreateEventRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateEvent.Command>();

            var result = await sender.Send(command);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}