using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Api.DTO;

public record class CreateMovieModel(string Title, string ReleasedDate, Person Director, Person[]? TopActors, int Rating, ProfileImage? Poster);