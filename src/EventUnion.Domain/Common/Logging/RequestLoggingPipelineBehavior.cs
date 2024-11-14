using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using IResult = CSharpFunctionalExtensions.IResult;

namespace EventUnion.CommonResources.Logging;

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    (ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest requestObject, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).DeclaringType?.Name ?? typeof(TRequest).Name;

        logger.LogInformation("[{RequestName}] processing request", requestName);

        var responseObject = await next();

        if (responseObject is IResult resultObject)
        {
            LogResult(resultObject, requestObject, responseObject, requestName);
        }
        else
        {
            logger.LogInformation("[{RequestName}] request completed", requestName);
        }

        return responseObject;
    }

    private void LogResult(IResult resultObject, TRequest requestObject, TResponse responseObject, string requestName)
    {
        if (resultObject.IsFailure && resultObject is IError<Error> resultWithError)
        {
            LogError(resultWithError, requestObject, requestName);
        }
        else switch (resultObject.IsSuccess)
        {
            case true when resultObject is IValue<object?> resultWithData:
                LogData(resultWithData, requestObject, requestName);
                break;
            case true:
            {
                using (LogContext.PushProperty(LoggingDefaults.InputLogProperty, requestObject, true))
                    logger.LogInformation("[{RequestName}] request completed with success", requestName);
                break;
            }
            default:
            {
                logger.LogInformation("[{RequestName}] request completed", requestName);

                using (LogContext.PushProperty(LoggingDefaults.InputLogProperty, requestObject, true))
                using (LogContext.PushProperty(LoggingDefaults.OutputLogProperty, responseObject, true))
                    logger.LogInformation("[{RequestName}] request completed with unstructured result", requestName);
                break;
            }
        }
    }

    private void LogError(IError<Error> resultWithError, TRequest requestObject, string requestName)
    {
        using (LogContext.PushProperty(LoggingDefaults.InputLogProperty, requestObject, true))
        using (LogContext.PushProperty(LoggingDefaults.ErrorLogProperty, resultWithError.Error, true))
            logger.LogError("[{RequestName}] request completed with error", requestName);
    }

    private void LogData(IValue<object?> resultWithValue, TRequest requestObject, string requestName)
    {
        using (LogContext.PushProperty(LoggingDefaults.InputLogProperty, requestObject, true))
        using (LogContext.PushProperty(LoggingDefaults.OutputLogProperty, resultWithValue.Value, true))
            logger.LogInformation("[{RequestName}] request completed with data result", requestName);
    }
}