using CinemaCat.Api.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Extensions;

public static class ApplicationResponseExtensions
{
    public static ActionResult<TResult> ToResult<TResult>(this ApplicationResponse<TResult> response)
    {
        return response.IsSuccess switch
        {
            true => new OkObjectResult(response.Result),
            false => new ObjectResult(response.Error) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }

    public static ActionResult<TResult> ToResult<TResult>(
        this ApplicationResponse<TResult> response,
        Func<TResult, ActionResult<TResult>> success,
        Func<string, ActionResult<TResult>> failure)
    {
        return response.IsSuccess switch
        {
            true => success(response.Result!),
            false => failure(response.Error!)
        };
    }
}