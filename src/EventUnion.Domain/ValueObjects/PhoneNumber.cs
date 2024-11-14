using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string Value { get; private set; } = null!;

    public static Result<PhoneNumber, Error> Create(string phoneNumber, string fieldName = "PhoneNumber")
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return CommonError.ValueIsEmpty(fieldName);

        var allZero = phoneNumber.Where(char.IsNumber).All(c => c == '0');
        if (allZero) return CommonError.ValueIsInvalid(fieldName);
        
        if (ContainsInvalidCharacter(phoneNumber))
            return CommonError.ValueIsInvalid(fieldName);

        phoneNumber = Sanitize(phoneNumber);
        
        return new PhoneNumber(phoneNumber);
    }
    
    public static string FormatPhoneNumber(string phoneNumber) =>
        string.Format("{0} ({1}) {2}-{3}",
            phoneNumber.Substring(0, 3),
            phoneNumber.Substring(3, 2),
            phoneNumber.Substring(5, 5),
            phoneNumber.Substring(10, 4));
    
    public static string Sanitize(string phoneNumber)
    {
        phoneNumber = StringUtils.RemoveNonNumericChars(phoneNumber);
        
        phoneNumber = phoneNumber.Length switch
        {
            11 => "+55" + phoneNumber,
            13 => "+" + phoneNumber,
            _ => phoneNumber
        };

        return phoneNumber;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private static bool ContainsInvalidCharacter(string phoneNumber) 
        => phoneNumber.Any(c => !char.IsDigit(c) && !char.IsWhiteSpace(c) && !c.Equals('+'));

    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private PhoneNumber() { }
}