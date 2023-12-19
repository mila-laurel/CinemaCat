using CinemaCat.Application.Handlers.Movies.GetMovie;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests;

public class GetMovieHandlerTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
    private readonly Mock<IDataBaseProvider<Movie>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<GetMovieRequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(exception);
        var _handler = new GetMovieHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Create<GetMovieRequest>();
        var movie = _fixture.Create<Movie>() with { Id = request.Id };
        _dataBaseProviderMock.Setup(m => m.GetByIdAsync(movie.Id))
            .ReturnsAsync(movie);
        var _handler = new GetMovieHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.Should().Be(movie);
    }
}
