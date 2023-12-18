namespace CinemaCat.Application.Interfaces
{
    public interface IBlobServiceProvider
    {
        Task<Guid> UploadAsync(Stream stream);
        Task<Stream> DownloadAsync(Guid id, bool preview = false);
    }
}
