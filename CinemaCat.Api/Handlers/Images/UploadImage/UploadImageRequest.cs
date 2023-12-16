using MediatR;

namespace CinemaCat.Api.Handlers.Images.UploadImage;

public class UploadImageRequest : IRequest<UploadImageResponse>
{
    public IFormFile File { get; init; }
}
