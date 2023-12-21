using CinemaCat.Application.Handlers.Movies.DeleteMovie;
using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Tests.Movies;

public class DeleteMovieHandlerTests
{
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<IDataBaseProvider<Movie>> _dataBaseProviderMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<DeleteMovieRequest>();
        var exception = new Exception("a weird error");
        _dataBaseProviderMock.Setup(m => m.RemoveAsync(It.IsAny<Guid>()))
            .ThrowsAsync(exception);
        var _handler = new DeleteMovieHandler(_dataBaseProviderMock.Object);

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
        var request = _fixture.Create<DeleteMovieRequest>();
        var _handler = new DeleteMovieHandler(_dataBaseProviderMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
    }
}
