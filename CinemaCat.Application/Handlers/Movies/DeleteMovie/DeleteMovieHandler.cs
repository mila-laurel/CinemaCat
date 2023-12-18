using CinemaCat.Infrastructure.Data;
using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Application.Handlers.Movies.DeleteMovie;

public class DeleteMovieHandler(IDataBaseProvider<Movie> moviesProvider)
    : ApplicationHandlerBase<DeleteMovieRequest, DeleteMovieResponse>
{
    protected override async Task<DeleteMovieResponse> HandleInternal(
        DeleteMovieRequest request,
        CancellationToken cancellationToken)
    {
        await moviesProvider.RemoveAsync(request.Id);
        return new DeleteMovieResponse();
    }
}
