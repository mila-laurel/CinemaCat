using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CinemaCat.Api.DTO;
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

        [HttpGet("{id}", Name = "GetImage")]
        [ProducesResponseType(typeof(Stream), 200)]
        public async Task<IActionResult> Get(Guid id, bool compressed = false)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blob = containerClient.GetBlobClient((compressed ? "compressed/" : "full/") + id);
            var stream = await blob.OpenReadAsync();
            return File(stream, "image/jpeg");
        }

        [HttpPost]
        [ProducesResponseType(typeof(UploadImageResponse), 200)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = new UploadImageResponse();
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
                await blob.UploadAsync(fullImageStream, new BlobHttpHeaders { ContentType = "image/jpeg" });
                await blobCompressed.UploadAsync(previewImageStream, new BlobHttpHeaders { ContentType = "image/jpeg" });

                result.FullImageUrl = $"{Request.Scheme}://{Request.Host}{Url.RouteUrl("GetImage", new { id = guid })}";
                result.CompressedImageUrl = $"{Request.Scheme}://{Request.Host}{Url.RouteUrl("GetImage", new { id = guid, compressed = true })}";
            }

            return Ok(result);
        }
    }
}
