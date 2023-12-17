using Azure.Storage.Blobs;

namespace CinemaCat.Api.Handlers.Images.GetImage;

public class GetImageHandler(BlobServiceClient blobServiceClient)
    : ApplicationHandlerBase<GetImageRequest, GetImageResponse>
{
    protected override async Task<GetImageResponse> HandleInternal(
        GetImageRequest request,
        CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("images");
        var blob = containerClient.GetBlobClient((request.IsPreview ? "compressed/" : "full/") + request.Id);
        var stream = await blob.OpenReadAsync();
        return new GetImageResponse { Result = stream };
    }
}
