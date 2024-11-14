using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class FullName : ValueObject
{
    // ReSharper disable once MemberCanBePrivate.Global
    public const int MaxLength = 256;

    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    // ReSharper disable once MemberCanBePrivate.Global
    public string Value { get; private set; } = null!;

    // ReSharper disable once UnusedMember.Global
    public static Result<FullName, Error> Create(string fullName, string fieldName = "FullName")
    {
        fullName = StringUtils.RemoveExtraSpaces(fullName);

        if (string.IsNullOrWhiteSpace(fullName))
            return CommonError.ValueIsEmpty(fieldName);

        if (fullName.Length > MaxLength)
            return CommonError.ValueIsTooLong(fieldName, MaxLength);

        return new FullName(fullName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private FullName(string fullName)
    {
        Value = fullName;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private FullName() { }
}