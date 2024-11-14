using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class Email : ValueObject
{
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    // ReSharper disable once MemberCanBePrivate.Global
    public string Value { get; private set; } = null!;

    // ReSharper disable once UnusedMember.Global
    public static Result<Email, Error> Create(string email, string fieldName = "Email")
    {
        if (string.IsNullOrWhiteSpace(email))
            return CommonError.ValueIsEmpty(fieldName);

        email = Sanitize(email);

        if (!new EmailAddressAttribute().IsValid(email))
            return CommonError.ValueIsInvalid(fieldName);
        
        return new Email(email);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static string Sanitize(string email) => StringUtils.RemoveExtraSpaces(email);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private Email(string email)
    {
        Value = email;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private Email() { }
}