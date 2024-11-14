using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable class UnusedMember.Global
// ReSharper disable once UnusedType.Global
public partial class Cpf : ValueObject
{
    public const int CpfLength = 11;
    
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string Value { get; private set; } = null!;
    public string FormattedValue => FormatCpf(Value);

    public static Result<Cpf, Error> Create(string cpf, string fieldName = "Cpf")
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return CommonError.ValueIsEmpty(fieldName);

        cpf = RemoveFormatting(cpf);

        if (!CpfValidationRegex().IsMatch(cpf))
            return CommonError.ValueIsInvalid(fieldName);

        if (cpf.Length != CpfLength || HasAllDigitsEqual(cpf) || !IsValidCpf(cpf))
            return CommonError.ValueIsInvalid(fieldName);

        return new Cpf(cpf);
    }

    public static string Sanitize(string cpf) => StringUtils.RemoveNonNumericChars(cpf);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private static string RemoveFormatting(string cpf)
    {
        return CfpRegex().Replace(cpf, "");
    }

    private static bool HasAllDigitsEqual(string cpf)
    {
        return cpf.All(c => c == cpf[0]);
    }
    
    private static bool IsValidCpf(string cpf)
    {
        int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var tempCpf = cpf[..9];
        var sum = 0;

        for (var i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        var remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        var digit = remainder.ToString();
        tempCpf += digit;
        sum = 0;

        for (var i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit += remainder.ToString();

        return cpf.EndsWith(digit);
    }

    private static string FormatCpf(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }
    
    private Cpf(string value)
    {
        Value = value;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private Cpf() { }

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex CfpRegex();
    
    [GeneratedRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$")]
    private static partial Regex CpfValidationRegex();
}
