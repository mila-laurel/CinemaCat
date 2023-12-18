using CinemaCat.Application.Interfaces;

namespace CinemaCat.Application.Handlers.Images.UploadImage;

public class UploadImageHandler(IBlobServiceProvider blobServiceProvider)
    : ApplicationHandlerBase<UploadImageRequest, UploadImageResponse>
{
    protected override async Task<UploadImageResponse> HandleInternal(
    UploadImageRequest request,
    CancellationToken cancellationToken)
    {
        var guid = await blobServiceProvider.UploadAsync(request.File);
        var result = new UploadedImagesInfo() { Id = guid.ToString() };
        return new UploadImageResponse { Result = result };
    }
}
