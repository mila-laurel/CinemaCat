using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Handlers.Movies.GetMovie;

public class GetMovieHandler(IDataBaseProvider<Movie> moviesProvider)
    : ApplicationHandlerBase<GetMovieRequest, GetMovieResponse>
{
    protected override async Task<GetMovieResponse> HandleInternalAsync(
        GetMovieRequest request,
        CancellationToken cancellationToken)
    {
        var result = await moviesProvider.GetByIdAsync(request.Id);
        if (result == null)
            return new GetMovieResponse { Error = "There is no items with such id" };
        return new GetMovieResponse { Result = result };
    }
}
