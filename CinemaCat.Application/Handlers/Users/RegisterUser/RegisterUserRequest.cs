using MediatR;

namespace CinemaCat.Application.Handlers.Users.RegisterUser;
public class RegisterUserRequest : IRequest<RegisterUserResponse>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string[]? Roles { get; set; }
    public required string Password { get; set; }
}
