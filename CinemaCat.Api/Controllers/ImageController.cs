using CinemaCat.Api.Extensions;
using CinemaCat.Api.Handlers.Images.GetImage;
using CinemaCat.Api.Handlers.Images.UploadImage;
using CinemaCat.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}", Name = "GetImage")]
    [ProducesResponseType(typeof(Stream), 200)]
    public async Task<ActionResult<Stream>> Get(Guid id, bool preview = false)
    {
        var req = new GetImageRequest { Id = id, IsPreview = preview };

        var result = await mediator.Send(req);
        return result.ToResult(x => File(x, "image/jpeg"), e => NotFound(e));
    }

    [HttpPost]
    [ProducesResponseType(typeof(UploadedImagesInfo), 200)]
    public async Task<ActionResult<UploadedImagesInfo>> UploadImage(IFormFile file)
    {
        var req = new UploadImageRequest() { File = file };
        var result = await mediator.Send(req);
        var guid = result.Result.Id;
        result.Result.FullImageUrl = $"{Request.Scheme}://{Request.Host}{Url.RouteUrl("GetImage", new { id = guid })}";
        result.Result.CompressedImageUrl = $"{Request.Scheme}://{Request.Host}{Url.RouteUrl("GetImage", new { id = guid, compressed = true })}";
        return result.ToResult();
    }
}
