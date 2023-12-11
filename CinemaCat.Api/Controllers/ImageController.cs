using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CinemaCat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ImageController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var results = new List<string>();

            await foreach (BlobItem blob in containerClient.GetBlobsAsync())
            {
                results.Add(blob.Name);
            }

            return results.ToArray();
        }

        [HttpPost]
        public async Task<Models.Image> UploadImage(IFormFile file)
        {
            var result = new Models.Image();
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var stream = file.OpenReadStream();
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
                var guid = Guid.NewGuid();
                var blob = containerClient.GetBlockBlobClient("full/" + guid);
                var blobCompressed = containerClient.GetBlockBlobClient("compressed/" + guid);
                await blob.UploadAsync(fullImageStream);
                await blobCompressed.UploadAsync(previewImageStream);

                result.FullImageUrl = blob.Uri.ToString();
                result.CompressedImageUrl = blobCompressed.Uri.ToString();
            }

            return result;
        }
    }
}
