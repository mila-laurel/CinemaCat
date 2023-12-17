using MediatR;

namespace CinemaCat.Api.Handlers.Movies.DeleteMovie;

public class DeleteMovieRequest : IRequest<DeleteMovieResponse>
{
    public Guid Id { get; set; }
}
