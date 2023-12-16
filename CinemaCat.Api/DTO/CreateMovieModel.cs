using CinemaCat.Api.Models;

namespace CinemaCat.Api.DTO;

public record class CreateMovieModel(string Title, string ReleasedDate, string DirectorName, int Rating);