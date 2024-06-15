using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Api.Entities;

public class User
{
    public User(Guid userId, string email, string password, string cryptKey, List<Interest> interests, Phone phone,
        Person person, Company company, UserAddress userAddress)
    {
        UserId = userId;
        Email = email;
        Password = password;
        CryptKey = cryptKey;
        Interests = interests;
        Phone = phone;
        Person = person;
        Company = company;
        UserAddress = userAddress;
    }

    [Key]
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CryptKey { get; set; }

    // Nav Properties
    public List<Interest> Interests { get; set; }
    public Phone Phone { get; set; }
    public Person Person { get; set; }
    public Company Company { get; set; }
    public UserAddress UserAddress { get; set; }
}

public class Phone
{
    public Phone(int phoneId, Guid userId, string value, User user)
    {
        PhoneId = phoneId;
        UserId = userId;
        Value = value;
        User = user;
    }

    [Key]
    public int PhoneId { get; set; }
    public Guid UserId { get; set; }
    public string Value { get; set; }

    // Nav Properties
    public User User { get; set; }
}

public class Interest
{
    public Interest(int interestId, Guid userId, string name, string value, User user)
    {
        InterestId = interestId;
        UserId = userId;
        Name = name;
        Value = value;
        User = user;
    }

    [Key]
    public int InterestId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    // Nav Properties
    public User User { get; set; }
}

public class UserAddress
{
    public UserAddress(Guid userId, Guid addressId, User user, Address address)
    {
        UserId = userId;
        AddressId = addressId;
        User = user;
        Address = address;
    }

    public Guid UserId { get; set; }
    public Guid AddressId { get; set; }

    // Nav Properties
    public User User { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public Address(Guid addressId, string zipCode, string street, string neighborhood, string number, string additionalInfo, string state, string country, UserAddress userAddress)
    {
        AddressId = addressId;
        ZipCode = zipCode;
        Street = street;
        Neighborhood = neighborhood;
        Number = number;
        AdditionalInfo = additionalInfo;
        State = state;
        Country = country;
        UserAddress = userAddress;
    }

    public Guid AddressId { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string Number { get; set; }
    public string AdditionalInfo { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    // Nav Properties
    public UserAddress UserAddress { get; set; }
}

public class Person
{
    public Person(Guid personId, Guid userId, string name, string cpf, DateTime birthdate, User user)
    {
        PersonId = personId;
        UserId = userId;
        Name = name;
        Cpf = cpf;
        Birthdate = birthdate;
        User = user;
    }

    public Guid PersonId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public DateTime Birthdate { get; set; }

    // Nav Properties
    public User User { get; set; }
}

public class Company
{
    public Company(Guid companyId, Guid userId, string cnpj, string legalName, string tradeName, string specialization, User user)
    {
        CompanyId = companyId;
        UserId = userId;
        Cnpj = cnpj;
        LegalName = legalName;
        TradeName = tradeName;
        Specialization = specialization;
        User = user;
    }

    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public string Cnpj { get; set; }
    public string LegalName { get; set; }
    public string TradeName { get; set; }
    public string Specialization { get; set; }

    // Nav Properties
    public User User { get; set; }
}