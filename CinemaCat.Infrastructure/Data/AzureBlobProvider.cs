using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CinemaCat.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CinemaCat.Infrastructure.Data
{
    public class AzureBlobProvider : IBlobServiceProvider
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobProvider(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task<Stream> DownloadAsync(Guid id, bool preview = false)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blob = containerClient.GetBlobClient((preview ? "compressed/" : "full/") + id);
            return await blob.OpenReadAsync();
        }

        public async Task<Guid> UploadAsync(Stream fileStream)
        {
            var guid = Guid.NewGuid();
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            using (var fullImageStream = new MemoryStream())
            using (var previewImageStream = new MemoryStream())
            using (var image = Image.Load(fileStream))
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
            return guid;
        }
    }
}
