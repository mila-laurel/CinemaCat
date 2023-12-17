using CinemaCat.Api.Models;
using MediatR;

namespace CinemaCat.Api.Handlers.Movies.CreateMovie;

public class CreateMovieRequest : IRequest<CreateMovieResponse>
{
    public string Title {  get; init; } 
    public string ReleasedDate { get; init; }
    public Person Director { get; init; }
    public int Rating { get; init; }
    public Person[]? TopActors { get; init; }
    public ProfileImage? Poster {  get; init; }
}
