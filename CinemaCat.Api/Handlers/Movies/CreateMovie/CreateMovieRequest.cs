using MediatR;

namespace CinemaCat.Api.Handlers.Movies.CreateMovie;

public class CreateMovieRequest : IRequest<CreateMovieResponse>
{
    public string Title {  get; init; } 
    public string ReleasedDate { get; init; }
    public string Director { get; init; }
    public int Rating { get; init; }
}
