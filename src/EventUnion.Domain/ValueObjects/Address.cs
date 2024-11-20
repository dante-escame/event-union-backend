using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public partial class Address : ValueObject
{
    // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
    // ReSharper disable MemberCanBePrivate.Global
    public string ZipCode { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string Neighborhood { get; private set; } = null!;
    public string Number { get; private set; } = null!;
    public string AdditionalInfo { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local
    // ReSharper restore MemberCanBePrivate.Global
    
    // ReSharper disable once UnusedMember.Global
    public static Result<Address, Error> Create(
        string zipCode,
        string street,
        string neighborhood,
        string number,
        string additionalInfo,
        string state,
        string country,
        string city,
        // ReSharper disable once UnusedParameter.Global
        string fieldName = "Address")
    {
        if (string.IsNullOrWhiteSpace(zipCode) || !IsNumeric(zipCode) || zipCode.Length != 8)
            return CommonError.ValueIsInvalid("Código Postal");

        if (string.IsNullOrWhiteSpace(street) || !IsAlphanumeric(street))
            return CommonError.ValueIsInvalid("Rua");

        if (string.IsNullOrWhiteSpace(neighborhood) || !IsAlphanumeric(neighborhood))
            return CommonError.ValueIsInvalid("Bairro");

        if (string.IsNullOrWhiteSpace(number) || !IsAlphanumeric(number))
            return CommonError.ValueIsInvalid("Número");

        if (string.IsNullOrWhiteSpace(state) || !IsAlphanumeric(state))
            return CommonError.ValueIsInvalid("Estado");

        if (string.IsNullOrWhiteSpace(country) || !IsAlphanumeric(country))
            return CommonError.ValueIsInvalid("País");

        if (string.IsNullOrWhiteSpace(city) || !IsAlphanumeric(city))
            return CommonError.ValueIsInvalid("Cidade");

        return new Address(zipCode, street, neighborhood, number, additionalInfo, state, country, city);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ZipCode;
        yield return Street;
        yield return Neighborhood;
        yield return Number;
        yield return AdditionalInfo;
        yield return State;
        yield return Country;
        yield return City;
    }

    private Address(string zipCode, string street, string neighborhood, string number, string additionalInfo, string state, string country, string city)
    {
        ZipCode = zipCode;
        Street = street;
        Neighborhood = neighborhood;
        Number = number;
        AdditionalInfo = additionalInfo;
        State = state;
        Country = country;
        City = city;
    }
    
    private static bool IsNumeric(string value)
    {
        return NumericRegex().IsMatch(value);
    }

    private static bool IsAlphanumeric(string value)
    {
        return AlphanumericRegex().IsMatch(value);
    }
    
    [GeneratedRegex(@"^\d+$")]
    private static partial Regex NumericRegex();
    [GeneratedRegex(@"^[a-zA-Z0-9À-ÿ\s]+$")]
    private static partial Regex AlphanumericRegex();

    // EFCore Constructor
    [ExcludeFromCodeCoverage]
    // ReSharper disable once UnusedMember.Local
    private Address() { }
}