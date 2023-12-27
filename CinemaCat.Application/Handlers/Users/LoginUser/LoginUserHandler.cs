using CinemaCat.Application.Configuration;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Identity;
using CinemaCat.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaCat.Application.Handlers.Users.LoginUser;
public class LoginUserHandler(IDataBaseProvider<ApplicationUser> userProvider, IOptions<JwtConfiguration> jwtConfiguration) 
    : ApplicationHandlerBase<LoginUserRequest, LoginUserResponse>
{
    protected override async Task<LoginUserResponse> HandleInternalAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await userProvider.GetAsync(u => u.Email == request.Email);
        if (existingUser == null || !existingUser.Any())
        {
            return new LoginUserResponse { Error = "User with that email does not exists" };
        }

        var passwordHash = existingUser.First().PasswordHash;
        var generatedHash = IdentityUtils.GetPasswordHash(request.Password + existingUser.First().Salt);
        if (passwordHash != generatedHash)
        {
            return new LoginUserResponse { Error = "The password is wrong" };
        }
        var roles = existingUser.First().Roles.Select(r => new Claim(ClaimTypes.Role, r));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, request.Email),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
            }.Union(roles)),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = jwtConfiguration.Value.Issuer,
            Audience = jwtConfiguration.Value.Audience,
            SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Value.Key)),
               SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return new LoginUserResponse { Result = stringToken };
    }
}
