using CinemaCat.Application.Configuration;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Identity;
using CinemaCat.Domain.Models;
using Microsoft.Extensions.Options;

namespace CinemaCat.Application.Handlers.Users.RegisterUser;
public class RegisterUserHandler(IDataBaseProvider<ApplicationUser> userProvider, IOptions<JwtConfiguration> configuration) 
    : ApplicationHandlerBase<RegisterUserRequest, RegisterUserResponse>
{
    protected override async Task<RegisterUserResponse> HandleInternalAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await userProvider.GetAsync(u => u.Email == request.Email);
        if (existingUser != null && existingUser.Any())
        {
            return new RegisterUserResponse { Error = "User with that email already exists" };
        }
        var applicationUser = new ApplicationUser
        {
            Name = request.Name,
            Email = request.Email,
            Salt = IdentityUtils.GenerateSalt()
        };
        applicationUser.PasswordHash = IdentityUtils.GetPasswordHash(request.Password + applicationUser.Salt);
        var details = await userProvider.CreateAsync(applicationUser);
        return new RegisterUserResponse();
    }
}
