using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace EventUnion.CommonResources;

[ExcludeFromCodeCoverage]
public static partial class StringUtils
{
    public static string FormatCpf(string cpf) =>
        $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}" +
        $"-{cpf.Substring(9, 2)}";

    public static string FormatCnpj(string cnpj) =>
        $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}" +
        $"/{cnpj.Substring(8, 4)}-{cnpj.Substring(12, 2)}";

    public static string FormatMaskCpf(string value) =>
        $"***.{value.Substring(3, 3)}.{value.Substring(6, 3)}-**";

    public static string FormatMaskCnpj(string value) =>
        $"{value.Substring(0, 2)}.***.***/****-{value.Substring(12, 2)}";
    
    public static string CompleteWithLeftZeros(this string entry, int totalWidth)
        => entry.PadLeft(totalWidth, '0');
    
    public static string RemoveNonNumericChars(string entry)
        => Regex.Replace(entry, "[^0-9]", "", RegexOptions.NonBacktracking).Trim();

    public static string RemoveNonAlphaNumericChars(string entry) 
        => Regex.Replace(entry, "[^A-Za-z0-9]", "", RegexOptions.NonBacktracking).Trim();

    public static string RemoveExtraSpaces(string input)
        => RemoveExtraSpacesRegex().Replace(input, " ").Trim();
    [GeneratedRegex(@"\s+")]
    private static partial Regex RemoveExtraSpacesRegex();
}