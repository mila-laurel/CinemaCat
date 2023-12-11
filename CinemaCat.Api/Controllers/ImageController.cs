using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CinemaCat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using System.Drawing;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<Models.Image> UploadImage([FromForm] IFormFile file)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var stream = file.OpenReadStream();
            var image = PlatformImage.FromStream(stream);
            IImage newImage = image.Downsize(100);

            var blob = containerClient.GetBlockBlobClient("full/" + file.FileName);
            await blob.UploadAsync(stream);
            var blobCompressed = containerClient.GetBlockBlobClient("compressed/" + file.FileName);
            using (MemoryStream memStream = new MemoryStream())
            {
                newImage.Save(memStream);
                await blobCompressed.UploadAsync(memStream);
            }
            var result = new Models.Image()
            {
                FullImageUrl = blob.Uri.ToString(),
                CompressedImageUrl = blobCompressed.Uri.ToString()
            };

            return result;
        }
    }
}
