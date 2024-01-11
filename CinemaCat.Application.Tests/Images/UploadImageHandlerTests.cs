using CinemaCat.Application.Handlers.Images.UploadImage;
using CinemaCat.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CinemaCat.Application.Tests.Images;

public class UploadImageHandlerTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly Mock<IBlobServiceProvider> _blobServiceMock = new();

    [Fact]
    public async Task Handle_ThrowsException_Error()
    {
        // arrange
        using var image = new Image<Rgb24>(5, 5);
        using var sourceStream = new MemoryStream();
        image.SaveAsPng(sourceStream);
        sourceStream.Position = 0;
        var request = new UploadImageRequest() { File = sourceStream };
        var exception = new Exception("a weird error");
        _blobServiceMock.Setup(m => m.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<MemoryStream>()))
            .ThrowsAsync(exception);
        var _handler = new UploadImageHandler(_blobServiceMock.Object);

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
        using var image = new Image<Rgb24>(1500, 500);
        using var sourceStream = new MemoryStream();
        image.SaveAsPng(sourceStream);
        sourceStream.Position = 0;
        var request = new UploadImageRequest() { File = sourceStream };
        Stream? savedFullStream = null;
        Stream? savedPreviewStream = null;

        _blobServiceMock.Setup(m => m.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<MemoryStream>()))
            .Callback<Stream, Stream>((fullStream, previewStream) =>
            {
                savedFullStream = new MemoryStream((fullStream as MemoryStream)?.ToArray() ?? Array.Empty<byte>());
                savedPreviewStream = new MemoryStream((previewStream as MemoryStream)?.ToArray() ?? Array.Empty<byte>());
            })
            .ReturnsAsync(Guid.Empty);
        var _handler = new UploadImageHandler(_blobServiceMock.Object);

        // act
        var response = await _handler.Handle(request, CancellationToken.None);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().NotBeNull();
        response.Result?.Id.Should().Be(Guid.Empty.ToString());
        savedPreviewStream.Should().NotBeNull();
        savedFullStream.Should().NotBeNull();
        var previewImamge = Image.Load(savedPreviewStream);
        var fullImamge = Image.Load(savedFullStream);
        fullImamge.Size().Should().Be(image.Size());
        var sourceRatio = (float)image.Height / image.Width;
        var previewRation = (float)previewImamge.Height / previewImamge.Width;
        sourceRatio.Should().Be(previewRation);
        previewImamge.Height.Should().Be(250);
    }
}
