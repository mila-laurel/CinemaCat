using MediatR;

namespace CinemaCat.Application.Handlers.Movies.GetMovie;

public class GetMovieRequest : IRequest<GetMovieResponse>
{
    public Guid Id { get; init; }
}
