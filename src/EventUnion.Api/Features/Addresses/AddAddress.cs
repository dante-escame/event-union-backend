using System.Data;
using CSharpFunctionalExtensions;
using EventUnion.Api.Features.Common;
using EventUnion.CommonResources;
using EventUnion.Domain.Addresses;
using EventUnion.Domain.Common.Interfaces;
using EventUnion.Domain.Users;
using EventUnion.Infrastructure;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace EventUnion.Api.Features.Addresses;

public static class AddAddress
{
    #region Endpoint
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Request
    {
        public Guid UserId { get; set; }
        public AddressPayload? Address { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global

    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UserId)
                .NotEmptyWithError();

            RuleFor(x => x.Address)
                .NotNullWithError()
                .MustBeValueObject(address => Domain.ValueObjects.Address.Create(address?.ZipCode ?? "",
                    address?.Street ?? "",
                    address?.Neighbourhood ?? "",
                    address?.Number ?? "",
                    address?.AddInfo ?? "",
                    address?.State ?? "",
                    address?.Country ?? "",
                    address?.City ?? ""));
        }
    }

    [Authorize]
    public class Endpoint(ISender sender) : Endpoint<Request, Result<ResourceLocator<Guid>, Error>>
    {
        public override void Configure()
        {
            Post("api/addresses");
            AllowAnonymous();
        }
        
        public override async Task<Result<ResourceLocator<Guid>, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var commandSave = new Command(
                req.UserId,
                Domain.ValueObjects.Address.Create(
                    req.Address?.ZipCode!,
                    req.Address?.Street!,
                    req.Address?.Neighbourhood!,
                    req.Address?.Number!,
                    req.Address?.AddInfo!,
                    req.Address?.State!,
                    req.Address?.Country!,
                    req.Address?.City!).Value);
            
            return await sender.Send(commandSave, ct);
        }
    }
    #endregion
    
    #region Handler
    // ReSharper disable once MemberCanBePrivate.Global
    public record Command(
        Guid UserId, 
        Domain.ValueObjects.Address Address) : IRequest<Result<ResourceLocator<Guid>, Error>>;
    
    internal class Handler(
        IUnitOfWork unitOfWork,
        EventUnionDbContext dbContext)
        : IRequestHandler<Command, Result<ResourceLocator<Guid>, Error>>
    {
        public async Task<Result<ResourceLocator<Guid>, Error>> Handle(Command request, CancellationToken ct)
        {
            var user = await dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId,
                    cancellationToken: ct);

            if (user is not null)
            {
                var address = new Address(Guid.NewGuid(),
                    request.Address);
                await unitOfWork.AddAsync(address, ct);

                var userAddress = new UserAddress(user, address);
                await unitOfWork.AddAsync(userAddress, ct);
                
                var result = await unitOfWork.SaveChangesAsync(ct);
                if (result.IsFailure)
                    return result.Error;

                return new ResourceLocator<Guid>(address.AddressId);
            }

            throw new DataException("Informações de usuário não encontradas.");
        }
    }
    #endregion
}