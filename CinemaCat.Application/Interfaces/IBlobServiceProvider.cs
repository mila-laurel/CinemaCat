namespace CinemaCat.Application.Interfaces
{
    public interface IBlobServiceProvider
    {
        Task<Guid> UploadAsync(Stream fullImageStream, Stream? previewImageStream = null);
        Task<Stream> DownloadAsync(Guid id, bool preview = false);
    }
}
