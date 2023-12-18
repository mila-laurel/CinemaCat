using MediatR;

namespace CinemaCat.Application.Handlers.Movies.SearchMovie;

public class SearchMovieRequest : IRequest<SearchMovieResponse>
{
    public string Title { get; init; }
}
