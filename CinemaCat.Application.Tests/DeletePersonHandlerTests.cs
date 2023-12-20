using CinemaCat.Application.Handlers.Persons.DeletePerson;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests;

public class DeletePersonHandlerTests
{
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<IDataBaseProvider<PersonDetails>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<DeletePersonRequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.RemoveAsync(It.IsAny<Guid>()))
            .ThrowsAsync(exception);
        var _handler = new DeletePersonHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be(exception.Message);
        response.Exception.Should().Be(exception);
    }

    [Fact]
    public async Task Handle_Success_ReturnsResult()
    {
        // arrange
        var request = _fixture.Create<DeletePersonRequest>();
        var _handler = new DeletePersonHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
    }
}
