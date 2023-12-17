using MediatR;

namespace CinemaCat.Api.Handlers.Movies.GetMovie;

public class GetMovieRequest : IRequest<GetMovieResponse>
{
    public Guid Id { get; init; }
}
