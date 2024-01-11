using CinemaCat.Api.DTO;
using CinemaCat.Api.Extensions;
using CinemaCat.Application.Handlers.Users.LoginUser;
using CinemaCat.Application.Handlers.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult<object>> Register([FromBody] CreateUserModel user)
    {
        var request = new RegisterUserRequest
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Roles = user.Roles
        };
        var result = await mediator.Send(request);
        return result.ToResult();
    }

    [HttpPost("signin")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserModel login)
    {
        var request = new LoginUserRequest
        {
            Email = login.Email,
            Password = login.Password
        };
        var result = await mediator.Send(request);
        return result.ToResult();
    }
}
