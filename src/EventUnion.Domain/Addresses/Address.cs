// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Addresses;

public class Address
{
    [NotMapped] public const int ZipCodeMaxLength = 8;
    [NotMapped] public const int StreetMaxLength = 256;
    [NotMapped] public const int NeighborhoodMaxLength = 256;
    [NotMapped] public const int NumberMaxLength = 8;
    [NotMapped] public const int StateMaxLength = 64;
    [NotMapped] public const int CountryMaxLength = 128;
    [NotMapped] public const int CityMaxLength = 256;
    
    public Guid AddressId { get; private set; }
    public string ZipCode { get; private set; }
    public string Street { get; private set; }
    public string Neighborhood { get; private set; }
    public string Number { get; private set; }
    public string AdditionalInfo { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }

    public Address(Guid addressId, ValueObjects.Address address)
    {
        AddressId = addressId;
        ZipCode = address.ZipCode;
        Street = address.Street;
        Neighborhood = address.Neighborhood;
        Number = address.Number;
        AdditionalInfo = address.AdditionalInfo;
        State = address.State;
        Country = address.Country;
        City = address.City;
    }
    
    [ExcludeFromCodeCoverage]
    private Address() { }
}