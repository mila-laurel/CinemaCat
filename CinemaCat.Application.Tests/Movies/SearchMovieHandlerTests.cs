using CinemaCat.Application.Handlers.Movies.SearchMovie;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;
using System.Linq.Expressions;

namespace CinemaCat.Application.Tests.Movies;

public class SearchMovieHandlerTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
    private readonly Mock<IDataBaseProvider<Movie>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<SearchMovieRequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Movie, bool>>>()))
            .ThrowsAsync(exception);
        var _handler = new SearchMovieHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Create<SearchMovieRequest>();
        var movie = _fixture.Create<Movie>() with { Title = request.Title ?? string.Empty };
        _dataBaseProviderMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Movie, bool>>>()))
            .ReturnsAsync(new List<Movie> { movie });
        var _handler = new SearchMovieHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result?.First().Should().Be(movie);
    }
}
