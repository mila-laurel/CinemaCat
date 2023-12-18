using MediatR;

namespace CinemaCat.Application.Handlers.Images.GetImage;

public class GetImageRequest : IRequest<GetImageResponse>
{
    public Guid Id { get; init; }

    public bool IsPreview { get; init; }
}
