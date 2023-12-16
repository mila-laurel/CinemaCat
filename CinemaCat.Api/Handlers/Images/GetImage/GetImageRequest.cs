using MediatR;

namespace CinemaCat.Api.Handlers.Images.GetImage;

public class GetImageRequest : IRequest<GetImageResponse>
{
    public Guid Id { get; init; }

    public bool IsPreview { get; init; }
}
