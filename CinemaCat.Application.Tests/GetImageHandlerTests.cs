using CinemaCat.Application.Handlers.Images.GetImage;
using CinemaCat.Application.Interfaces;

namespace CinemaCat.Application.Tests;

public class GetImageHandlerTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly Mock<IBlobServiceProvider> _blobServiceMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        var request = _fixture.Create<GetImageRequest>();
        var exception = new Exception("a weird error");
        _blobServiceMock.Setup(m => m.DownloadAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
            .ThrowsAsync(exception);
        var _handler = new GetImageHandler(_blobServiceMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.Error.Should().Be(exception.Message);
        response.Exception.Should().Be(exception);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_Success_ReturnsResult(bool preview)
    {
        // arrange
        var request = new GetImageRequest
        {
            Id = Guid.NewGuid(),
            IsPreview = preview
        };
        var imageStream = new MemoryStream([1, 2, 3, 4, 5]);
        _blobServiceMock.Setup(m => m.DownloadAsync(request.Id, preview))
            .ReturnsAsync(imageStream);
        var _handler = new GetImageHandler(_blobServiceMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeSameAs(imageStream);        
    }
}
