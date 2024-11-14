using CSharpFunctionalExtensions;
using EventUnion.Api.Features.Common;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;
using EventUnion.Domain.Common.Interfaces;
using EventUnion.Domain.Events;
using EventUnion.Domain.Users;
using EventUnion.Domain.ValueObjects;
using EventUnion.Infrastructure;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Events;

public static class AddEvent
{
    #region Endpoint
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Request
    {
        public Guid UserOwnerId { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EventTypeId { get; set; }
        public int? TargetId { get; set; }
        public bool? Private { get; set; }
        public AddressPayload? Address { get; set; }
        public List<string>? Tags { get; set; }
        public List<string>? ParticipantEmails { get; set; }
    }
    
    public class Response
    {
        public string? Identifier { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    
    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UserOwnerId)
                .NotEmptyWithError();
            
            RuleFor(x => x.Image)
                .NotEmptyWithError();
            
            RuleFor(x => x.Name)
                .NotEmptyWithError()
                .MustBeValueObject(name => FullName.Create(name!));

            RuleFor(x => x.Description)
                .NotEmptyWithError();

            RuleFor(x => x.StartDate)
                .NotNullWithError()
                .LessThan(x => x.EndDate).WithMessage("Data de início deve ser inferno a data final.");

            RuleFor(x => x.EndDate)
                .NotNullWithError();
            
            RuleFor(x => x.EventTypeId)
                .NotNullWithError();
            
            RuleFor(x => x.TargetId)
                .NotNullWithError();
            
            RuleFor(x => x.Private)
                .NotNullWithError();
            
            RuleFor(x => x.Address)
                .NotNullWithError()
                .MustBeValueObject(address => Address.Create(address?.ZipCode ?? "",
                    address?.Street ?? "",
                    address?.Neighbourhood ?? "",
                    address?.Number ?? "",
                    address?.AddInfo ?? "",
                    address?.State ?? "",
                    address?.Country ?? "",
                    address?.City ?? ""));

            RuleFor(x => x.Tags)
                .NotNullWithError()
                .Must(tags => tags!.Count > 0).WithMessage("Selecione ao menos um interesse.");

            RuleFor(x => x.ParticipantEmails)
                .NotNullWithError()
                .Must(emails => emails!.Count > 0).WithMessage("Selecione ao menos um e-mail.");
        }
    }

    [Authorize]
    public class Endpoint(ISender sender) : Endpoint<Request, Result<ResourceLocator<Response>, Error>>
    {
        public override void Configure()
        {
            Post("api/events");
        }
        
        public override async Task<Result<ResourceLocator<Response>, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var commandSave = new Command(
                req.UserOwnerId, 
                req.Image!, 
                req.Name!, 
                req.Description!, 
                req.StartDate, 
                req.EndDate, 
                Address.Create(
                    req.Address?.ZipCode!,
                    req.Address?.Street!,
                    req.Address?.Neighbourhood!,
                    req.Address?.Number!,
                    req.Address?.AddInfo!,
                    req.Address?.State!,
                    req.Address?.Country!,
                    req.Address?.City!).Value, 
                req.EventTypeId!.Value, 
                req.TargetId!.Value, 
                req.Private!.Value, 
                req.Tags!, 
                req.ParticipantEmails!);
            
            return await sender.Send(commandSave, ct);
        }
    }
    #endregion
    
    #region Handler
    public record Command(
        Guid UserOwnerId, 
        string Image,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        Address Address,
        int EventTypeId,
        int TargetId,
        bool Private,
        List<string> Tags,
        List<string> ParticipantEmails) : IRequest<Result<ResourceLocator<Response>, Error>>;
    
    internal class Handler(
        IUnitOfWork unitOfWork,
        EventUnionDbContext dbContext)
        : IRequestHandler<Command, Result<ResourceLocator<Response>, Error>>
    {
        public async Task<Result<ResourceLocator<Response>, Error>> Handle(Command request, CancellationToken ct)
        {
            var userOwner = await dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.UserId == request.UserOwnerId, ct);
            if (userOwner is null)
                return CommonError.EntityNotFound("Usuário", request.UserOwnerId.ToString());
            
            var target = await dbContext.Set<Target>()
                .FirstOrDefaultAsync(x => x.TargetId == request.TargetId, ct);
            if (target is null)
                return CommonError.EntityNotFound("Público Alvo", request.TargetId.ToString());
            
            var eventType = await dbContext.Set<EventType>()
                .FirstOrDefaultAsync(x => x.EventTypeId == request.EventTypeId, ct);
            if (eventType is null)
                return CommonError.EntityNotFound("Tipo de Evento", request.EventTypeId.ToString());

            var address = new Domain.Addresses.Address(Guid.NewGuid(), request.Address);
            
            var eventEntity = new Event(Guid.NewGuid(), 
                userOwner,
                eventType,
                FullName.Create(request.Name).Value,
                target,
                address,
                request.Description,
                Period.Create(request.StartDate, request.EndDate).Value,
                request.Private,
                request.Image);
            
            await unitOfWork.AddAsync(eventEntity, ct);
            
            foreach (var tagName in request.Tags)
            {
                var tag = await dbContext.Set<Tag>()
                    .FirstOrDefaultAsync(x => x.Name == tagName,
                        cancellationToken: ct);
                if (tag is not null)
                    await unitOfWork.AddAsync(new EventTag(tag, eventEntity), ct);
            }

            foreach (var email in request.ParticipantEmails)
            {
                var eventUser = await dbContext.Set<User>()
                    .FirstOrDefaultAsync(x => x.Email.Value == email,
                        cancellationToken: ct);
                if (eventUser is not null)
                    await unitOfWork.AddAsync(new EventUser(eventEntity, eventUser, email), ct);
            }

            var result = await unitOfWork.SaveChangesAsync(ct);
            if (result.IsFailure)
                return result.Error;

            var response = new Response
            {
                Identifier = eventEntity.EventId.ToString()
            };
            
            return new ResourceLocator<Response>(response);
        }
    }
    #endregion
}