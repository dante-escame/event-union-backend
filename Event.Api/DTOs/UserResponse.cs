namespace Event.Api.DTOs;

public class UserResponse
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? CryptKey { get; set; }

    public PhoneResponse? Phone { get; set; }
    public PersonResponse? Person { get; set; }
    public CompanyResponse? Company { get; set; }
    public AddressResponse? Address { get; set; }
    public List<InterestResponse>? Interests { get; set; }
}

public class PhoneResponse
{
    public string? Value { get; set; }
}

public class InterestResponse
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}

public class AddressResponse
{
    public string? ZipCode { get; set; }
    public string? Street { get; set; }
    public string? Neighborhood { get; set; }
    public string? Number { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}

public class PersonResponse
{
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public DateTime? Birthdate { get; set; }
}

public class CompanyResponse
{
    public string? Cnpj { get; set; }
    public string? LegalName { get; set; }
    public string? TradeName { get; set; }
    public string? Specialization { get; set; }
}