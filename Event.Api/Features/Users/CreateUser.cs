using Carter;
using Event.Api.DTOs;
using Event.Api.Entities;
using Event.Api.Infrastructure;
using Event.Api.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace Event.Api.Features.Users;

public static class CreateUser
{
    public class Command(UserRequest userRequest) : IRequest<Result<Guid>>
    {
        public UserRequest UserRequest { get; set; } = userRequest;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserRequest).SetValidator(new UserRequestValidator());
        }
    }

    internal sealed class Handler(EventDbContext dbContext, IValidator<Command> validator)
        : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            var request = command.UserRequest;
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<Guid>(new Error("CreateUser.Validation",
                    validationResult.ToString()));

            var userId = Guid.NewGuid();
            var addressId = Guid.NewGuid();
            
            var phoneEntity = new Phone(default, userId, request.Phone!.Value!);
            var personEntity = new Person(Guid.NewGuid(), userId, request.Person!.Name!, request.Person!.Cpf!,
                request.Person.Birthdate!.Value);
            var companyEntity = new Company(Guid.NewGuid(), userId, request.Company!.Cnpj!, request.Company!.LegalName!,
                request.Company!.TradeName!, request.Company!.Specialization!);
            var addressEntity = new Address(addressId, request.UserAddress!.ZipCode!, request.UserAddress!.Street!, 
                request.UserAddress!.Neighborhood!, request.UserAddress!.Number!, request.UserAddress.AdditionalInfo!, 
                request.UserAddress.State!, request.UserAddress.Country!);
            var userAddressEntity = new UserAddress(userId, addressId)
            {
                Address = addressEntity
            };
            
            var i = 1;
            var interestEntities = new List<Interest>();
            if (request.Interests is not null)
                foreach (var interest in request.Interests)
                {
                    interestEntities.Add(new Interest(i, userId, interest.Name!, interest.Value!));
                    i++;
                }

            var userEntity = new User(userId, 
                request.Email!, 
                request.Password!, 
                request.CryptKey!)
            {
                Phone = phoneEntity,
                Person = personEntity,
                Company = companyEntity,
                UserAddress = userAddressEntity,
                Interests = interestEntities
            };
            
            await dbContext.AddAsync(userEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return userEntity.UserId;
        }
    }
}

public class CreateEventEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (UserRequest request, ISender sender) =>
        {
            var command = new CreateUser.Command(request);

            var result = await sender.Send(command);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}