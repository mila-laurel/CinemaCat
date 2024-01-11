using CinemaCat.Domain.Models;
using MediatR;

namespace CinemaCat.Application.Handlers.Movies.CreateMovie;

public class CreateMovieRequest : IRequest<CreateMovieResponse>
{
    public required string Title { get; init; }
    public string? ReleasedDate { get; init; }
    public required Person Director { get; init; }
    public int Rating { get; init; }
    public Person[]? TopActors { get; init; }
    public ProfileImage? Poster { get; init; }
}
