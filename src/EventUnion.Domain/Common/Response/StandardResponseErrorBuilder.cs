using EventUnion.Domain.Common.Errors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace EventUnion.CommonResources.Response;

public static class StandardResponseErrorBuilder
{
    public static object BuildResponse(
        List<ValidationFailure> validationFailures, HttpContext ctx, int statusCode)
    {
        List<Problem> problems = [];

        var validationFailuresGroup = validationFailures.GroupBy(vf => vf.PropertyName)
            .Select(g => (PropertyName: g.Key, Failures: g.ToList())).ToList();

        foreach (var (propertyName, failures) in validationFailuresGroup)
        {
            var deserializedErrors = DeserializeErrors(failures);
            problems.AddRange(deserializedErrors.Select(error => new Problem(propertyName, error)));
        }
        
        return StandardResponse.FromBadRequest(problems);
    }

    private static List<Error> DeserializeErrors(
        List<ValidationFailure> validationFailures)
    {
        List<Error> deserializedErrors = [];
        foreach (var failure in validationFailures)
        {
            var deserializedError = Error.Deserialize(failure.ErrorMessage);

            deserializedErrors.Add(deserializedError ?? CommonError.RequestIsInvalid(failure.ErrorMessage));
        }
        return deserializedErrors;
    }
}