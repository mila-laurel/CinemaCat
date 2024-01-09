using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Utils;
public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object? value) : base(value)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }

    public InternalServerErrorObjectResult() : this(null)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
