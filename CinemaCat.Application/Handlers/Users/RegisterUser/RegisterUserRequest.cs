using MediatR;

namespace CinemaCat.Application.Handlers.Users.RegisterUser;
public class RegisterUserRequest : IRequest<RegisterUserResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string[]? Roles { get; set; }
    public string Password { get; set; }
}
