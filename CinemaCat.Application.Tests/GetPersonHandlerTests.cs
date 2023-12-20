using CinemaCat.Application.Handlers.Persons.GetPerson;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests;

public class GetPersonHandlerTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
    private readonly Mock<IDataBaseProvider<PersonDetails>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<GetPersonRequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(exception);
        var _handler = new GetPersonHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Create<GetPersonRequest>();
        var person = _fixture.Create<PersonDetails>() with { Id = request.Id };
        _dataBaseProviderMock.Setup(m => m.GetByIdAsync(person.Id))
            .ReturnsAsync(person);
        var _handler = new GetPersonHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.Should().Be(person);
    }
}
