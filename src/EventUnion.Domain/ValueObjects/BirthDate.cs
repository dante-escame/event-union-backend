using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class Birthdate : ValueObject
{
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    // ReSharper disable once MemberCanBePrivate.Global
    public DateTime Value { get; private set; }

    // ReSharper disable once UnusedMember.Global
    public static Result<Birthdate, Error> Create(DateTime birthdate, string fieldName = "Birthdate")
    {
        if (birthdate > DateTime.Now || birthdate < new DateTime(1900, 1, 1))
            return CommonError.ValueIsInvalid(fieldName);
        
        return new Birthdate(birthdate);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private Birthdate(DateTime value)
    {
        Value = value;
    }

    // EFCore Constructor
    [ExcludeFromCodeCoverage]
    // ReSharper disable once UnusedMember.Local
    private Birthdate() { }
}