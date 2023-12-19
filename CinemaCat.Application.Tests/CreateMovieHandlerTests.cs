using CinemaCat.Application.Handlers.Movies.CreateMovie;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests;

public class CreateMovieHandlerTests
{
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<IDataBaseProvider<Movie>> _dataBaseProviderMock = new();
    
    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Build<CreateMovieRequest>()
            .With(x => x.ReleasedDate, "2023-10-09")
            .Create();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.CreateAsync(It.IsAny<Movie>()))
            .ThrowsAsync(exception);
        CreateMovieHandler _handler = new CreateMovieHandler(_dataBaseProviderMock.Object);

        // act
        var response =  await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be(exception.Message);
        response.Exception.Should().Be(exception);
    }

    [Fact]
    public async Task Handle_Success_ReturnsResult()
    {
        // arrange
        var request = _fixture.Build<CreateMovieRequest>()
            .With(x => x.ReleasedDate, "2023-10-09")
            .Create();
        _dataBaseProviderMock.Setup(m => m.CreateAsync(It.IsAny<Movie>()))
            .ReturnsAsync((Movie x) => x);
        CreateMovieHandler _handler = new CreateMovieHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.Title.Should().Be(request.Title);
        response.Result.Rating.Should().Be(request.Rating);
        response.Result.ReleasedDate.Should().Be(DateOnly.Parse(request.ReleasedDate));
        response.Result.Director.Should().Be(request.Director); 
        response.Result.TopActors.SequenceEqual(request.TopActors);
        response.Result.Poster.Should().Be(request.Poster);
    }
}