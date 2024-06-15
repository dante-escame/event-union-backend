using System.ComponentModel.DataAnnotations;

namespace Event.Api.Entities;

public class User(
    Guid userId,
    string email,
    string password,
    string cryptKey,
    List<Interest> interests,
    Phone phone,
    Person person,
    Company company,
    UserAddress userAddress)
{
    [Key]
    public Guid UserId { get; set; } = userId;

    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string CryptKey { get; set; } = cryptKey;

    // Nav Properties
    public List<Interest> Interests { get; set; } = interests;
    public Phone Phone { get; set; } = phone;
    public Person Person { get; set; } = person;
    public Company Company { get; set; } = company;
    public UserAddress UserAddress { get; set; } = userAddress;
}

public class Phone(int phoneId, Guid userId, string value, User user)
{
    [Key]
    public int PhoneId { get; set; } = phoneId;

    public Guid UserId { get; set; } = userId;
    public string Value { get; set; } = value;

    // Nav Properties
    public User User { get; set; } = user;
}

public class Interest(int interestId, Guid userId, string name, string value, User user)
{
    [Key]
    public int InterestId { get; set; } = interestId;

    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;

    // Nav Properties
    public User User { get; set; } = user;
}

public class UserAddress(Guid userId, Guid addressId, User user, Address address)
{
    public Guid UserId { get; set; } = userId;
    public Guid AddressId { get; set; } = addressId;

    // Nav Properties
    public User User { get; set; } = user;
    public Address Address { get; set; } = address;
}

public class Address(
    Guid addressId,
    string zipCode,
    string street,
    string neighborhood,
    string number,
    string additionalInfo,
    string state,
    string country,
    UserAddress userAddress)
{
    public Guid AddressId { get; set; } = addressId;
    public string ZipCode { get; set; } = zipCode;
    public string Street { get; set; } = street;
    public string Neighborhood { get; set; } = neighborhood;
    public string Number { get; set; } = number;
    public string AdditionalInfo { get; set; } = additionalInfo;
    public string State { get; set; } = state;
    public string Country { get; set; } = country;

    // Nav Properties
    public UserAddress UserAddress { get; set; } = userAddress;
}

public class Person(Guid personId, Guid userId, string name, string cpf, DateTime birthdate, User user)
{
    public Guid PersonId { get; set; } = personId;
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public string Cpf { get; set; } = cpf;
    public DateTime Birthdate { get; set; } = birthdate;

    // Nav Properties
    public User User { get; set; } = user;
}

public class Company(
    Guid companyId,
    Guid userId,
    string cnpj,
    string legalName,
    string tradeName,
    string specialization,
    User user)
{
    public Guid CompanyId { get; set; } = companyId;
    public Guid UserId { get; set; } = userId;
    public string Cnpj { get; set; } = cnpj;
    public string LegalName { get; set; } = legalName;
    public string TradeName { get; set; } = tradeName;
    public string Specialization { get; set; } = specialization;

    // Nav Properties
    public User User { get; set; } = user;
}