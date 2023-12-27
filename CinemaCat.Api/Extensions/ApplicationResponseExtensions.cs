using CinemaCat.Api.Utils;
using CinemaCat.Application.Handlers;
using CinemaCat.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Extensions;

public static class ApplicationResponseExtensions
{
    public static ActionResult<object> ToResult(this ApplicationResponse response)
    {
        var genericResponse = new ApplicationResponse<object>
        {
            Error = response.Error,
            Exception = response.Exception,
            Result = null
        };

        return genericResponse.ToResult();
    }

    public static ActionResult<TResult> ToResult<TResult>(this ApplicationResponse<TResult> response)
    {
        return response.ToResult(r => new OkObjectResult(r), e => new InternalServerErrorObjectResult(e));
    }

    public static ActionResult<TResult> ToResult<TResult>(
        this ApplicationResponse<TResult> response,
        Func<TResult, ActionResult<TResult>> success,
        Func<string, ActionResult<TResult>> failure)
    {
        ActionResult<TResult> GetFailureResult()
        {
            return response.Exception switch
            {
                AuthorizationException => new UnauthorizedObjectResult("You don't have a permission to access the method"),
                _ => failure(response.Error!)
            };
        }

        return response.IsSuccess switch
        {
            true => success(response.Result!),
            false => GetFailureResult()
        };
    }
}