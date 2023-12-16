using MediatR;

namespace CinemaCat.Api.Handlers.Movies.SearchMovie;

public class SearchMovieRequest : IRequest<SearchMovieResponse>
{
    public string Title { get; init; }
}
