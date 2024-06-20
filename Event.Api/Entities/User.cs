using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Event.Api.Entities;

public class User(
    Guid userId,
    string email,
    string password,
    string cryptKey)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; } = userId;

    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string CryptKey { get; set; } = cryptKey;

    // Nav Properties
    public ICollection<Interest> Interests { get; set; }
    public Phone Phone { get; set; }
    public Person Person { get; set; }
    public Company Company { get; set; }
    public UserAddress UserAddress { get; set; }
}

public class Phone(Guid userId, string value)
{
    [Key]
    public Guid UserId { get; set; } = userId;
    public string Value { get; set; } = value;

    // Nav Properties
    public User User { get; set; }
}

[PrimaryKey("InterestId", "UserId")]
public class Interest(int interestId, Guid userId, string name, string value)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InterestId { get; set; } = interestId;

    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;

    // Nav Properties
    public User User { get; set; }
}

[PrimaryKey("UserId", "AddressId")]
public class UserAddress(Guid userId, Guid addressId)
{
    public Guid UserId { get; set; } = userId;
    public Guid AddressId { get; set; } = addressId;

    // Nav Properties
    public User User { get; set; }
    public Address Address { get; set; }
}

public class Address(
    Guid addressId,
    string zipCode,
    string street,
    string neighborhood,
    string number,
    string additionalInfo,
    string state,
    string country)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AddressId { get; set; } = addressId;
    
    public string ZipCode { get; set; } = zipCode;
    public string Street { get; set; } = street;
    public string Neighborhood { get; set; } = neighborhood;
    public string Number { get; set; } = number;
    public string AdditionalInfo { get; set; } = additionalInfo;
    public string State { get; set; } = state;
    public string Country { get; set; } = country;

    // Nav Properties
    public UserAddress UserAddress { get; set; }
}

public class Person(Guid personId, Guid userId, string name, string cpf, DateTime birthdate)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PersonId { get; set; } = personId;
    
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public string Cpf { get; set; } = cpf;
    public DateTime Birthdate { get; set; } = birthdate;

    // Nav Properties
    public User User { get; set; }
}

public class Company(
    Guid companyId,
    Guid userId,
    string cnpj,
    string legalName,
    string tradeName,
    string specialization)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CompanyId { get; set; } = companyId;
    
    public Guid UserId { get; set; } = userId;
    public string Cnpj { get; set; } = cnpj;
    public string LegalName { get; set; } = legalName;
    public string TradeName { get; set; } = tradeName;
    public string Specialization { get; set; } = specialization;

    // Nav Properties
    public User User { get; set; }
}