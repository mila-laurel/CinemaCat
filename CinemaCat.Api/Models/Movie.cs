namespace CinemaCat.Api.Models;

public record class Movie
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public DateOnly ReleasedDate { get; init; }

    public string[] Countries { get; init; }

    public required Person Director { get; init; }

    public required Person[] TopActors { get; init; }

    public int Rating { get; init; }

    public Image Poster { get; set; }
}