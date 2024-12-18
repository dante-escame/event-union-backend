using CSharpFunctionalExtensions;
using EventUnion.Api.Features.Common;
using EventUnion.CommonResources;
using EventUnion.Domain.Addresses;
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

namespace EventUnion.Api.Features.Users;

public static class AddPerson
{
    #region Endpoint
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Request
    {
        public UserPayload? User { get; set; }
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public List<string>? Tags { get; set; }

        public record UserPayload
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    
    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmptyWithError()
                .MustBeValueObject(name => FullName.Create(name!));
            
            RuleFor(x => x.Cpf)
                .NotEmptyWithError()
                .MustBeValueObject(cpf => Cpf.Create(cpf!));
            
            RuleFor(x => x.Birthdate)
                .NotNullWithError()
                .MustBeValueObject(x => Birthdate.Create(x));
            
            RuleFor(x => x.Cpf)
                .NotEmptyWithError()
                .MustBeValueObject(cpf => Cpf.Create(cpf!));
            
            RuleFor(x => x.Phone)
                .NotEmptyWithError()
                .MustBeValueObject(phoneNumber => PhoneNumber.Create(phoneNumber!));
            
            RuleFor(x => x.Tags)
                .NotNullWithError()
                .Must(tags => tags!.Count > 0).WithMessage("Selecione ao menos um interesse.");
            
            RuleFor(x => x.User)
                .NotNullWithError();
            
            RuleFor(x => x.User!.Email)
                .NotEmptyWithError()
                .MustBeValueObject(email => Email.Create(email!));
            
            RuleFor(x => x.User!.Password)
                .NotEmptyWithError();
        }
    }

    // ReSharper disable once UnusedType.Global
    [Authorize]
    public class Endpoint(ISender sender) : Endpoint<Request, Result<ResourceLocator<Guid>, Error>>
    {
        public override void Configure()
        {
            Post("api/people");
            AllowAnonymous();
        }
        
        public override async Task<Result<ResourceLocator<Guid>, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var commandSave = new Command(
                FullName.Create(req.Name!).Value, Cpf.Create(req.Cpf!).Value, Birthdate.Create(req.Birthdate).Value, 
                req.Tags, Email.Create(req.User?.Email!).Value, PhoneNumber.Create(req.Phone!).Value, req.User?.Password);
            
            return await sender.Send(commandSave, ct);
        }
    }
    #endregion
    
    #region Handler
    public record Command(
        FullName? Name, Cpf? Cpf, Birthdate? BirthDate, List<string>? Tags,
        Email? Email, PhoneNumber? Phone, string? Password) : IRequest<Result<ResourceLocator<Guid>, Error>>;
    
    // ReSharper disable once UnusedType.Global
    internal class Handler(
        IUnitOfWork unitOfWork,
        EventUnionDbContext dbContext)
        : IRequestHandler<Command, Result<ResourceLocator<Guid>, Error>>
    {
        public async Task<Result<ResourceLocator<Guid>, Error>> Handle(Command request, CancellationToken ct)
        {
            var (pwd, key, iv) = EncryptionHelper.EncryptPassword(request.Password!);

            var person = new Person(Guid.NewGuid(), request.Email!, pwd, key,
                iv, Guid.NewGuid(), request.Name!,
                request.Cpf!, request.BirthDate!);
            
            await unitOfWork.AddAsync(person, ct);

            foreach (var tagName in request.Tags ?? [])
            {
                var tag = await dbContext.Set<Tag>()
                    .FirstOrDefaultAsync(x => x.Name == tagName,
                        cancellationToken: ct);
                if (tag is not null)
                    await unitOfWork.AddAsync(new UserTag(tag, person), ct);
            }
            
            var phone = new Phone(person,
                request.Phone!.Value);
            await unitOfWork.AddAsync(phone, ct);
            
            var result = await unitOfWork.SaveChangesAsync(ct);
            if (result.IsFailure)
                return result.Error;

            return new ResourceLocator<Guid>(person.UserId);
        }
    }
    #endregion
}