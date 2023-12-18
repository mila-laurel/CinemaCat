using CinemaCat.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CinemaCat.Application.Handlers.Images.UploadImage;

public class UploadImageHandler(IBlobServiceProvider blobServiceProvider)
    : ApplicationHandlerBase<UploadImageRequest, UploadImageResponse>
{
    protected override async Task<UploadImageResponse> HandleInternal(
    UploadImageRequest request,
    CancellationToken cancellationToken)
    {
        var result = new UploadedImagesInfo();
        using (var fullImageStream = new MemoryStream())
        using (var previewImageStream = new MemoryStream())
        using (var image = Image.Load(request.File))
        {
            image.Save(fullImageStream, new JpegEncoder());
            var ratio = image.Height / image.Width;
            var clone = image.Clone(x => x.Resize(250 * ratio, 250));
            clone.Save(previewImageStream, new JpegEncoder());

            previewImageStream.Position = 0;
            fullImageStream.Position = 0;
            var guid = await blobServiceProvider.UploadAsync(fullImageStream, previewImageStream);
            result.Id = guid.ToString();
        }
        return new UploadImageResponse { Result = result };
    }
}
