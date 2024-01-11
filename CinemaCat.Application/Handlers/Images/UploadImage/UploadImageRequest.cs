using MediatR;

namespace CinemaCat.Application.Handlers.Images.UploadImage;

public class UploadImageRequest : IRequest<UploadImageResponse>
{
    public required Stream File { get; init; }
}
