using CSharpFunctionalExtensions;
using EventUnion.Api.Features.Common;
using EventUnion.CommonResources;
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

public static class AddCompany
{
    #region Endpoint
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public record Request
    {
        public UserPayload? User { get; set; }
        public string? LegalName { get; set; }
        public string? TradeName { get; set; }
        public string? Cnpj { get; set; }
        public string? Specialization { get; set; }
        public List<string>? Tags { get; set; }

        // ReSharper disable once ClassNeverInstantiated.Global
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

            RuleFor(x => x.LegalName)
                .NotEmptyWithError()
                .MustBeValueObject(legalName => FullName.Create(legalName!));

            RuleFor(x => x.TradeName)
                .NotEmptyWithError()
                .MustBeValueObject(tradeName => FullName.Create(tradeName!));

            RuleFor(x => x.Cnpj)
                .NotEmptyWithError()
                .MustBeValueObject(cnpj => Cnpj.Create(cnpj!));

            RuleFor(x => x.Specialization)
                .NotEmptyWithError();

            RuleFor(x => x.Tags)
                .NotNullWithError()
                .Must(tags => tags!.Count > 0).WithMessage("A lista de tags não pode estar vazia.");
            
            RuleFor(x => x.User)
                .NotNullWithError();

            RuleFor(x => x.User!.Email)
                .NotEmptyWithError()
                .MustBeValueObject(email => Email.Create(email!));

            RuleFor(x => x.User!.Password)
                .NotEmptyWithError();
        }
    }

    [Authorize]
    public class Endpoint(ISender sender) : Endpoint<Request, Result<ResourceLocator<Guid>, Error>>
    {
        public override void Configure()
        {
            Post("api/companies");
            AllowAnonymous();
        }
        
        public override async Task<Result<ResourceLocator<Guid>, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var commandSave = new Command(
                FullName.Create(req.LegalName!).Value, FullName.Create(req.TradeName!).Value, 
                Cnpj.Create(req.Cnpj!).Value, req.Specialization,
                req.Tags, Email.Create(req.User?.Email!).Value, req.User?.Password);
            
            return await sender.Send(commandSave, ct);
        }
    }
    #endregion
    
    #region Handler
    public record Command(
        FullName? LegalName, FullName? TradeName, Cnpj? Cnpj, string? Specialization,
        List<string>? Tags, Email? Email, string? Password) : IRequest<Result<ResourceLocator<Guid>, Error>>;
    
    internal class Handler(
        IUnitOfWork unitOfWork,
        EventUnionDbContext dbContext)
        : IRequestHandler<Command, Result<ResourceLocator<Guid>, Error>>
    {
        public async Task<Result<ResourceLocator<Guid>, Error>> Handle(Command request, CancellationToken ct)
        {
            var (pwd, key, iv) = EncryptionHelper.EncryptPassword(request.Password!);
            
            var company = new Company(Guid.NewGuid(), request.Email!, pwd, key,
                iv, Guid.NewGuid(), request.Cnpj!, request.LegalName!, 
                request.TradeName!, request.Specialization ?? "");
            
            await unitOfWork.AddAsync(company, ct);

            foreach (var tagName in request.Tags ?? [])
            {
                var tag = await dbContext.Set<Tag>()
                    .FirstOrDefaultAsync(x => x.Name == tagName,
                        cancellationToken: ct);
                if (tag is not null)
                    await unitOfWork.AddAsync(new UserTag(tag, company), ct);
            }
            
            var result = await unitOfWork.SaveChangesAsync(ct);
            if (result.IsFailure)
                return result.Error;

            return new ResourceLocator<Guid>(company.UserId);
        }
    }
    #endregion
}