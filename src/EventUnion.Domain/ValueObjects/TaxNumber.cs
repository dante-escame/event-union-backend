using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class TaxNumber : ValueObject
{
    private TaxNumber(string value)
    {
        Value = value;
    }
    
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string Value { get; private set; } = null!;
    
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public TaxNumberType Type => Value.Length == 11 
        ? TaxNumberType.Cpf 
        : TaxNumberType.Cnpj;
    
    // ReSharper disable once UnusedMember.Global
    public string FormattedValue => Type == TaxNumberType.Cnpj 
        ? StringUtils.FormatCnpj(Value)
        : StringUtils.FormatCpf(Value);

    // ReSharper disable once UnusedMember.Global
    public string MaskedValue => Type == TaxNumberType.Cnpj 
            ? StringUtils.FormatMaskCnpj(Value)
            : StringUtils.FormatMaskCpf(Value);

    // ReSharper disable once UnusedMember.Global
    public static Result<TaxNumber, Error> Create(string taxNumber, string fieldName = "TaxNumber")
    {
        if (string.IsNullOrWhiteSpace(taxNumber))
            return CommonError.ValueIsEmpty(fieldName);

        taxNumber = StringUtils.RemoveNonNumericChars(taxNumber.Trim());
        taxNumber = StringUtils.RemoveExtraSpaces(taxNumber);
        
        if (taxNumber.Length == Cpf.CpfLength)
        {
            var cpfResult = Cpf.Create(taxNumber);

            if (cpfResult.IsFailure) return cpfResult.Error;

            return new TaxNumber(cpfResult.Value.Value);
        }
        
        if (taxNumber.Length == Cnpj.Length)
        {
            var cnpjResult = Cnpj.Create(taxNumber);

            if (cnpjResult.IsFailure) return cnpjResult.Error;

            return new TaxNumber(cnpjResult.Value.Value);
        }

        return CommonError.ValueIsInvalid(fieldName);
    }
    
    public enum TaxNumberType
    {
        Cpf,
        Cnpj
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private TaxNumber() { }
}