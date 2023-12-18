using MediatR;

namespace CinemaCat.Application.Handlers.Images.UploadImage;

public class UploadImageRequest : IRequest<UploadImageResponse>
{
    public Stream File { get; init; }
}
