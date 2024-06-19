using FluentValidation;

namespace Event.Api.DTOs;

public class UserRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? CryptKey { get; set; }

    public CreatePhoneRequest? Phone { get; set; }
    public CreatePersonRequest? Person { get; set; }
    public CreateCompanyRequest? Company { get; set; }
    public CreateUserAddressRequest? UserAddress { get; set; }
    public List<CreateInterestRequest>? Interests { get; set; }
}

public class CreatePhoneRequest
{
    public string? Value { get; set; }
}

public class CreateInterestRequest
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}

public class CreateUserAddressRequest
{
    public string? ZipCode { get; set; }
    public string? Street { get; set; }
    public string? Neighborhood { get; set; }
    public string? Number { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}

public class CreatePersonRequest
{
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public DateTime? Birthdate { get; set; }
}

public class CreateCompanyRequest
{
    public string? Cnpj { get; set; }
    public string? LegalName { get; set; }
    public string? TradeName { get; set; }
    public string? Specialization { get; set; }
}

public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password é obrigatório.");
            RuleFor(x => x.CryptKey).NotEmpty().WithMessage("CryptKey é obrigatório.");

            RuleFor(x => x.Phone).SetValidator(new CreatePhoneRequestValidator()!);
            RuleFor(x => x.Person).SetValidator(new CreatePersonRequestValidator()!);
            RuleFor(x => x.Company).SetValidator(new CreateCompanyRequestValidator()!);
            RuleFor(x => x.UserAddress).SetValidator(new CreateUserAddressRequestValidator()!);

            RuleFor(x => x.Interests).Must(interests => interests!= null && interests.Count > 0).WithMessage("Ao menos um Interest é obrigatório.");
            RuleForEach(x => x.Interests).SetValidator(new CreateInterestRequestValidator());
        }
    }

    public class CreatePhoneRequestValidator : AbstractValidator<CreatePhoneRequest>
    {
        public CreatePhoneRequestValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Valor para Phone é obrigatório.");
        }
    }

    public class CreateInterestRequestValidator : AbstractValidator<CreateInterestRequest>
    {
        public CreateInterestRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Interest name é obrigatório.");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Interest value é obrigatório.");
        }
    }

    public class CreateUserAddressRequestValidator : AbstractValidator<CreateUserAddressRequest>
    {
        public CreateUserAddressRequestValidator()
        {
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code é obrigatório.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street é obrigatório.");
            RuleFor(x => x.Neighborhood).NotEmpty().WithMessage("Neighborhood é obrigatório.");
            RuleFor(x => x.Number).NotEmpty().WithMessage("Number é obrigatório.");
            RuleFor(x => x.State).NotEmpty().WithMessage("State é obrigatório.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country é obrigatório.");
        }
    }

    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name é obrigatório.");
            RuleFor(x => x.Cpf).NotEmpty().WithMessage("CPF é obrigatório.");
            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("Birthdate é obrigatório.");
        }
    }

    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(x => x.Cnpj).NotEmpty().WithMessage("CNPJ é obrigatório.");
            RuleFor(x => x.LegalName).NotEmpty().WithMessage("Legal name é obrigatório.");
            RuleFor(x => x.TradeName).NotEmpty().WithMessage("Trade name é obrigatório.");
            RuleFor(x => x.Specialization).NotEmpty().WithMessage("Specialization é obrigatório.");
        }
    }