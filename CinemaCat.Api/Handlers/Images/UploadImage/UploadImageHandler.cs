using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CinemaCat.Api.DTO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CinemaCat.Api.Handlers.Images.UploadImage;

public class UploadImageHandler(BlobServiceClient blobServiceClient)
    : ApplicationHandlerBase<UploadImageRequest, UploadImageResponse>
{
    protected override async Task<UploadImageResponse> HandleInternal(
    UploadImageRequest request,
    CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var result = new UploadedImagesInfo() { Id = guid.ToString() };
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("images");
        var stream = request.File.OpenReadStream();
        using (var fullImageStream = new MemoryStream())
        using (var previewImageStream = new MemoryStream())
        using (var image = Image.Load(stream))
        {
            image.Save(fullImageStream, new JpegEncoder());
            var ratio = image.Height / image.Width;
            var clone = image.Clone(x => x.Resize(250 * ratio, 250));
            clone.Save(previewImageStream, new JpegEncoder());

            previewImageStream.Position = 0;
            fullImageStream.Position = 0;           
            var blob = containerClient.GetBlockBlobClient("full/" + guid);
            var blobCompressed = containerClient.GetBlockBlobClient("compressed/" + guid);
            await blob.UploadAsync(fullImageStream, new BlobHttpHeaders { ContentType = "image/jpeg" });
            await blobCompressed.UploadAsync(previewImageStream, new BlobHttpHeaders { ContentType = "image/jpeg" });          
        }

        return new UploadImageResponse { Result = result };
    }
}
