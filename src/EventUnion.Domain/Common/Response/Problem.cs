using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.CommonResources.Response;

[ExcludeFromCodeCoverage]
public record Problem
{
    public Problem(Error error)
    {
        Source = error.Code == CommonError.InternalServerErrorCode
            ? ServerErrorSource
            : FailErrorSource;

        Code = error.Code;
        Description = error.Message;
    }

    public Problem(string fieldName, Error error)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        Code = error.Code;
        Description = error.Message;
        Source = $"{ClientErrorSource}:{ConvertPathToCamelCase(fieldName)}";
    }

    private const string ClientErrorSource = "request";
    private const string ServerErrorSource = "server";
    private const string FailErrorSource = "fail";

    [JsonPropertyName("source")]
    public string? Source { get; }

    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    private static string ConvertPathToCamelCase(string fieldName)
    {
        var fields = fieldName.Split('.');

        for (var i = 0; i < fields.Length; i++)
            fields[i] = ConvertFieldNameToCamelCase(fields[i]);

        return string.Join('.', fields);
    }

    private static string ConvertFieldNameToCamelCase(string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : char.ToLower(input[0]) + input[1..];
}