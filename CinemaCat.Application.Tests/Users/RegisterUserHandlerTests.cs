using CinemaCat.Application.Handlers.Users.RegisterUser;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Exceptions;
using CinemaCat.Domain.Models;
using System.Linq.Expressions;

namespace CinemaCat.Application.Tests.Users;
public class RegisterUserHandlerTests
{
    private readonly Mock<IDataBaseProvider<ApplicationUser>> _userProviderMock = new();
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<RegisterUserRequest>();
        var man = _fixture.Create<ApplicationUser>();
        man.Email = request.Email;
        _userProviderMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(new List<ApplicationUser> { man });
        var _handler = new RegisterUserHandler(_userProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be("User with that email already exists");
        response.Exception.Should().BeOfType<AuthorizationException>();
    }

    [Fact]
    public async Task Handle_Success_ReturnsResult()
    {
        // arrange
        var request = _fixture.Create<RegisterUserRequest>();
        _userProviderMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(new List<ApplicationUser>());
        _userProviderMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>()))
           .ReturnsAsync((ApplicationUser x) => x);
        var _handler = new RegisterUserHandler(_userProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
    }
}
