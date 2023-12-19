using CinemaCat.Application.Handlers.Persons.CreatePerson;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests;

public class CreatePersonHandlerTests
{
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<IDataBaseProvider<PersonDetails>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Build<CreatePersonRequest>()
            .With(x => x.DateOfBirth, "1980-10-09")
            .Create();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.CreateAsync(It.IsAny<PersonDetails>()))
            .ThrowsAsync(exception);
        var _handler = new CreatePersonHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Build<CreatePersonRequest>()
            .With(x => x.DateOfBirth, "1980-10-09")
            .Create();
        _dataBaseProviderMock.Setup(m => m.CreateAsync(It.IsAny<PersonDetails>()))
            .ReturnsAsync((PersonDetails x) => x);
        var _handler = new CreatePersonHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.Name.Should().Be(request.Name);
        response.Result.PlaceOfBirth.Should().Be(request.PlaceOfBirth);
        response.Result.DateOfBirth.Should().Be(DateOnly.Parse(request.DateOfBirth));
        response.Result.Photo.Should().Be(request.Photo);
    }
}
