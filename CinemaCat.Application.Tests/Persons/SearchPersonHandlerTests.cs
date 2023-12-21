using CinemaCat.Application.Handlers.Persons.SearchPerson;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;
using System.Linq.Expressions;

namespace CinemaCat.Application.Tests.Persons;

public class SearchPersonHandlerTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
    private readonly Mock<IDataBaseProvider<PersonDetails>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<SearchPersonPequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<PersonDetails, bool>>>()))
            .ThrowsAsync(exception);
        var _handler = new SearchPersonHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Create<SearchPersonPequest>();
        var man = _fixture.Create<PersonDetails>() with { Name = request.Name };
        _dataBaseProviderMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<PersonDetails, bool>>>()))
            .ReturnsAsync(new List<PersonDetails> { man });
        var _handler = new SearchPersonHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.First().Should().Be(man);
    }
}
