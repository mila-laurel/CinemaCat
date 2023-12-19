using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Handlers.Movies.CreateMovie;

public class CreateMovieHandler(IDataBaseProvider<Movie> moviesProvider)
    : ApplicationHandlerBase<CreateMovieRequest, CreateMovieResponse>
{
    protected override async Task<CreateMovieResponse> HandleInternalAsync(
        CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        var newValue = new Movie
        {
            Title = request.Title,
            Rating = request.Rating,
            ReleasedDate = DateOnly.Parse(request.ReleasedDate),
            Director = request.Director,
            TopActors = request.TopActors,
            Poster = request.Poster
        };
        var movie = await moviesProvider.CreateAsync(newValue);
        return new CreateMovieResponse { Result = movie };
    }

}
