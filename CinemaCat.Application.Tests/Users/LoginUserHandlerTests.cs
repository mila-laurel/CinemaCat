﻿using CinemaCat.Application.Configuration;
using CinemaCat.Application.Handlers.Users.LoginUser;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Exceptions;
using CinemaCat.Domain.Identity;
using CinemaCat.Domain.Models;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CinemaCat.Application.Tests.Users;
public class LoginUserHandlerTests
{
    private readonly Mock<IDataBaseProvider<ApplicationUser>> _userProviderMock = new();
    private readonly Mock<IOptions<JwtConfiguration>> _jwtConfiguration = new();
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ThrowsException_ErrorEmail()
    {
        // arrange
        var request = _fixture.Create<LoginUserRequest>();
        _userProviderMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(new List<ApplicationUser>());
        var _handler = new LoginUserHandler(_userProviderMock.Object, _jwtConfiguration.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be("User with that email does not exists");
        response.Exception.Should().BeOfType<AuthorizationException>();
    }

    [Fact]
    public async Task Handle_ThrowsException_ErrorPassword()
    {
        // arrange
        var request = _fixture.Create<LoginUserRequest>();
        var man = _fixture.Create<ApplicationUser>();
        man.Email = request.Email;
        _userProviderMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(new List<ApplicationUser>() { man });
        var _handler = new LoginUserHandler(_userProviderMock.Object, _jwtConfiguration.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be("The password is wrong");
        response.Exception.Should().BeOfType<AuthorizationException>();
    }

    [Fact]
    public async Task Handle_Success_ReturnsResult()
    {
        // arrange
        var request = _fixture.Create<LoginUserRequest>();
        var man = _fixture.Create<ApplicationUser>();
        man.Email = request.Email;
        man.PasswordHash = IdentityUtils.GetPasswordHash(request.Password + man.Salt);
        _userProviderMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(new List<ApplicationUser>() { man });
        _jwtConfiguration.SetupGet(m => m.Value)
            .Returns(new JwtConfiguration
            {
                Issuer = "unitTestIssuer",
                Audience = "unitTestAudience",
                Key = "unitTestsKeyUnitTestsKeyUnitTestsKeyUnitTestsKeyUnitTestsKeyUnitTestsKeyUnitTestsKey"
            });
        var _handler = new LoginUserHandler(_userProviderMock.Object, _jwtConfiguration.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNullOrWhiteSpace();

        if (response.Result != null)
        {
            var splittedString = response.Result.Split('.');
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(splittedString[1]));
            var document = JsonNode.Parse(json);
            if (document != null)
            {
                document["iss"]?.ToString().Should().Be("unitTestIssuer");
                document["aud"]?.ToString().Should().Be("unitTestAudience");
            }
        }
    }
}
