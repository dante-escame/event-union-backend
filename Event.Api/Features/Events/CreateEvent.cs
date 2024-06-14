using Carter;
using Event.Api.Database;
using Event.Api.DTOs;
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

    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly EventDbContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(EventDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error("CreateEvent.Validation",
                    validationResult.ToString()));
            }

            var eventEntity = new Entities.Event()
            {
                EventId = request.EventId
            };
            
            _dbContext.Add(eventEntity);

            await _dbContext.SaveChangesAsync(cancellationToken);

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