using MediatR;

namespace CinemaCat.Application.Handlers.Movies.SearchMovie;

public class SearchMovieRequest : IRequest<SearchMovieResponse>
{
    public required string Title { get; init; }
}
