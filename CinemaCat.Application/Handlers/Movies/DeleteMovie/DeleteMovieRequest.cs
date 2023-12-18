using MediatR;

namespace CinemaCat.Application.Handlers.Movies.DeleteMovie;

public class DeleteMovieRequest : IRequest<DeleteMovieResponse>
{
    public Guid Id { get; set; }
}
