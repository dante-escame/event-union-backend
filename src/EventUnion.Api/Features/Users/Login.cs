using System.Security.Authentication;
using CSharpFunctionalExtensions;
using EventUnion.Api.Features.Common;
using EventUnion.CommonResources;
using EventUnion.Domain.Users;
using EventUnion.Domain.ValueObjects;
using EventUnion.Infrastructure;
using FastEndpoints;
using FastEndpoints.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global

namespace EventUnion.Api.Features.Users;

public static class Login
{
    #region Endpoint
    
    // ReSharper disable class UnusedAutoPropertyAccessor.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Request
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmptyWithError()
                .MustBeValueObject(email => Email.Create(email!));
            
            RuleFor(x => x.Password)
                .NotEmptyWithError();
        }
    }

    // ReSharper disable once UnusedType.Global
    [Authorize]
    public class Endpoint(ISender sender) : Endpoint<Request, Result<ResourceLocator<string>, Error>>
    {
        public override void Configure()
        {
            Post("api/login");
            AllowAnonymous();
        }
        
        public override async Task<Result<ResourceLocator<string>, Error>> ExecuteAsync(Request req, CancellationToken ct)
        {
            var commandSave = new Command(
                req.Email, req.Password);
            
            return await sender.Send(commandSave, ct);
        }
    }
    #endregion
    
    #region Handler
    public record Command(
        string? Email, string? Password) : IRequest<Result<ResourceLocator<string>, Error>>;
    
    // ReSharper disable once UnusedType.Global
    internal class Handler(EventUnionDbContext dbContext)
        : IRequestHandler<Command, Result<ResourceLocator<string>, Error>>
    {
        public async Task<Result<ResourceLocator<string>, Error>> Handle(Command request, CancellationToken ct)
        {
            var user = await dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Email.Value == request.Email,
                cancellationToken: ct);
            
            if (user is not null)
            {
                var password = EncryptionHelper.DecryptPassword(user.Password, user.CriptKey, user.Iv);
                
                if (password != request.Password) throw new AuthenticationException("Usuário/senha inválidos!");
                
                var jwtToken = JwtBearer.CreateToken(
                    o =>
                    {
                        o.SigningKey = Random256BytesKey.Value; 
                        o.ExpireAt = DateTime.UtcNow.AddDays(1);
                        o.User.Roles.Add("Manager", "Auditor");
                        o.User.Claims.Add(("UserName", request.Email!));
                        o.User["UserId"] = "001";
                    });
                
                return new ResourceLocator<string>(jwtToken);
            }

            throw new AuthenticationException("Usuário/senha inválidos!");
        }
    }
    #endregion
}