using CinemaCat.Application.Interfaces;

namespace CinemaCat.Application.Handlers.Images.GetImage;

public class GetImageHandler(IBlobServiceProvider blobServiceProvider)
    : ApplicationHandlerBase<GetImageRequest, GetImageResponse>
{
    protected override async Task<GetImageResponse> HandleInternalAsync(
        GetImageRequest request,
        CancellationToken cancellationToken)
    {
        var stream = await blobServiceProvider.DownloadAsync(request.Id, request.IsPreview);
        return new GetImageResponse { Result = stream };
    }
}
