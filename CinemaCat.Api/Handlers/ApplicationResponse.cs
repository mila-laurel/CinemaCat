namespace CinemaCat.Api.Handlers;

public class ApplicationResponse
{
    public bool IsSuccess => string.IsNullOrEmpty(Error);
    public string? Error { get; init; }
    public Exception? Exception { get; init; }
}

public class ApplicationResponse<T> : ApplicationResponse
{
    public T? Result { get; set; }
}