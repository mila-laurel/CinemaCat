using CinemaCat.Infrastructure.Data;
using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Application.Handlers.Movies.SearchMovie;

public class SearchMovieHandler(IDataBaseProvider<Movie> moviesProvider)
    : ApplicationHandlerBase<SearchMovieRequest, SearchMovieResponse>
{
    protected override async Task<SearchMovieResponse> HandleInternal(
        SearchMovieRequest request,
        CancellationToken cancellationToken)
    {
        var result = await moviesProvider.GetAsync(movie => movie.Title.Contains(request.Title));
        if (result.Count == 0)
            return new SearchMovieResponse { Error = "No movies with such title" };
        return new SearchMovieResponse { Result = result };
    }
}
