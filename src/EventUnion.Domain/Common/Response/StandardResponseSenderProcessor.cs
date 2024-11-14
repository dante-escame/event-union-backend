using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.Domain.Common.Errors;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using IResult = CSharpFunctionalExtensions.IResult;

namespace EventUnion.CommonResources.Response;

// ReSharper disable once ClassNeverInstantiated.Global
[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public sealed class StandardResponseSenderProcessor<TUser> : GlobalPostProcessor<StandardProcessorState<TUser>> where TUser : class
{
    public override async Task PostProcessAsync(IPostProcessorContext context,
        StandardProcessorState<TUser> processorState, CancellationToken ct)
    {
        if (context.HasExceptionOccurred) return;

        if (context.HttpContext.ResponseStarted()) return;

        var contextResponse = context.Response;
        if (contextResponse is not IResult resultObject) return;

        if (resultObject.IsFailure && resultObject is IError<Error> resultWithError)
        {
            var message = GetFailStandardMessageByContext(context.HttpContext, processorState);

            await SendError(context, message, resultWithError, ct);
        }
        else if (resultObject.IsSuccess && resultObject is IValue<object?> resultWithData)
        {
            var message = GetSuccessStandardMessage(context.HttpContext, processorState);

            await SendSuccess(context, resultWithData, message, ct);
        }
    }

    private static string GetFailStandardMessageByContext(HttpContext contextHttpContext,
        StandardProcessorState<TUser> processorState)
    {
        if (processorState.CustomStandardMessageDefined)
            return processorState.CustomMessage;

        return contextHttpContext.Request.Method switch
        {
            var method when method == HttpMethods.Get => StandardMessage.QueryFail(),
            var method when method == HttpMethods.Post => StandardMessage.CreateFail(processorState.EntityName),
            var method when method == HttpMethods.Put => StandardMessage.UpdateFail(),
            var method when method == HttpMethods.Delete => StandardMessage.DeleteFail(),
            _ => StandardMessage.CommandFail()
        };
    }

    private static string GetSuccessStandardMessage(HttpContext contextHttpContext,
        StandardProcessorState<TUser> processorState)
    {
        if (processorState.CustomStandardMessageDefined)
            return processorState.CustomMessage;

        return contextHttpContext.Request.Method switch
        {
            var method when method == HttpMethods.Get => StandardMessage.QuerySuccess(),
            var method when method == HttpMethods.Post => StandardMessage.CreateSuccess(processorState.EntityName),
            var method when method == HttpMethods.Put => StandardMessage.UpdateSuccess(),
            var method when method == HttpMethods.Delete => StandardMessage.DeleteSuccess(),
            _ => StandardMessage.CommandSuccess()
        };
    }

    private static async Task SendSuccess(IPostProcessorContext context,
        IValue<object?> resultWithData, string message, CancellationToken ct)
    {
        await context.HttpContext.Response.SendOkAsync(
            StandardResponse.FromSuccess(resultWithData.Value!, message), cancellation: ct);
    }

    private static async Task SendError(IPostProcessorContext context, string message, IError<Error> resultWithError,
        CancellationToken ct)
    {
        switch (resultWithError.Error.Code)
        {
            case CommonError.EntityNotFoundErrorCode:
                await context.HttpContext.Response.SendAsync(
                    StandardResponse.FromSuccess(null, StandardMessage.QueryFail()), cancellation: ct);
                break;
            case CommonError.InternalServerErrorCode:
                await context.HttpContext.Response.SendAsync(
                    StandardResponse.FromInternalServerError(message),
                    StatusCodes.Status500InternalServerError, cancellation: ct);
                break;
            default:
                await context.HttpContext.Response.SendAsync(
                    StandardResponse.FromBadRequest(message), 
                    cancellation: ct);
                break;
        }
    }
}