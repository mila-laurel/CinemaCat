using MediatR;

namespace CinemaCat.Application.Handlers.Users.LoginUser;
public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
