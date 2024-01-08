using CinemaCat.Api.Extensions;
using CinemaCat.Application.Handlers.Images.GetImage;
using CinemaCat.Application.Handlers.Images.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ImageController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}", Name = "GetImage")]
    [Authorize(Roles = "user")]
    [ProducesResponseType(typeof(Stream), 200)]
    public async Task<ActionResult<Stream>> Get(Guid id, bool preview = false)
    {
        var req = new GetImageRequest { Id = id, IsPreview = preview };

        var result = await mediator.Send(req);
        return result.ToResult(x => File(x, "image/jpeg"), e => NotFound(e));
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(UploadedImagesInfo), 200)]
    public async Task<ActionResult<UploadedImagesInfo>> UploadImage(IFormFile file)
    {
        using var inputStream = file.OpenReadStream();
        var req = new UploadImageRequest() { File = inputStream };
        var result = await mediator.Send(req);
        var guid = result.Result.Id;
        result.Result.FullImageUrl = Url.RouteUrl("GetImage", new { id = guid }, protocol: Request.Scheme);
        result.Result.CompressedImageUrl = Url.RouteUrl("GetImage", new { id = guid, compressed = true }, protocol: Request.Scheme);
        return result.ToResult();
    }
}
