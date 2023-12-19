using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Handlers.Movies.DeleteMovie;

public class DeleteMovieHandler(IDataBaseProvider<Movie> moviesProvider)
    : ApplicationHandlerBase<DeleteMovieRequest, DeleteMovieResponse>
{
    protected override async Task<DeleteMovieResponse> HandleInternalAsync(
        DeleteMovieRequest request,
        CancellationToken cancellationToken)
    {
        await moviesProvider.RemoveAsync(request.Id);
        return new DeleteMovieResponse();
    }
}
