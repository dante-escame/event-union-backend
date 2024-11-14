using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class Cnpj : ValueObject
{
    public const int Length = 14;

    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string Value { get; private set; } = null!;

    // ReSharper disable once UnusedMember.Global
    public static Result<Cnpj, Error> Create(string cnpj, string fieldName = "Cnpj")
    {
        cnpj = Sanitize(cnpj);
        
        var isValid = cnpj.Length == Length 
            && HaveValidCnpjFormat(cnpj) 
            && NotAllEqual(cnpj) 
            && IsValidCnpj(cnpj);
        
        if (!isValid) return CommonError.ValueIsInvalid(fieldName);
        
        return new Cnpj(cnpj);
    }

    public static string Sanitize(string cnpj) 
        => StringUtils.RemoveNonNumericChars(cnpj);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private static bool HaveValidCnpjFormat(string cnpj)
    {
        return Regex.IsMatch(cnpj, 
            @"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}", 
            RegexOptions.NonBacktracking);
    }

    private static bool NotAllEqual(string cnpj)
    {
        return cnpj.Distinct().Count() != 1;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        int[] weight1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] weight2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        var cnpjWithoutDigits = cnpj.Substring(0, 12);
        var actualDigits = cnpj.Substring(12, 2);

        var calculatedDigits = CalculateDigit(cnpjWithoutDigits, weight1) 
                               + CalculateDigit(cnpjWithoutDigits 
                                                + CalculateDigit(cnpjWithoutDigits, weight1), weight2);

        return actualDigits == calculatedDigits;
    }

    private static string CalculateDigit(string value, int[] weight)
    {
        var sum = value.Select((t, i) => (t - '0') * weight[i]).Sum();
        var remainder = sum % 11;
        return (remainder < 2 ? 0 : 11 - remainder).ToString();
    }

    private Cnpj(string cnpj)
    {
        Value = cnpj;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private Cnpj() { }
}
