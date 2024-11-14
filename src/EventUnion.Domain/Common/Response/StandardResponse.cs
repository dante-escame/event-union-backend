using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

// ReSharper disable UnusedMember.Global

namespace EventUnion.CommonResources.Response;

public class StandardResponse(
    int statusCode,
    string message,
    object? data)
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; private set; } = statusCode;

    [JsonPropertyName("message")]
    public string Message { get; private set; } = message;

    [JsonPropertyName("data")]
    public object? Data { get; private set; } = data;

    public static object FromBadRequest(string message)
    {
        return Results.BadRequest(new StandardResponse(
            StatusCodes.Status400BadRequest,
            message, 
            null
        ));
    }
    
    public static object FromBadRequest(List<Problem> problems)
    {
        var firstErrorMessage = GetFirstErrorMessage(problems);

        return new StandardResponse(
            StatusCodes.Status400BadRequest,
            firstErrorMessage,
            null
        );
    }

    public static object FromInternalServerError(string message)
    {
        return new StandardResponse(
            StatusCodes.Status500InternalServerError,
            message, 
            null
        );
    }

    public static object FromSuccess(object? data, string message = "")
    {
        return new StandardResponse(
            statusCode: StatusCodes.Status200OK,
            message,
            data);
    }
    
    private static string GetFirstErrorMessage(List<Problem> problems)
    {
        return problems.FirstOrDefault()!.Description;
    }
}