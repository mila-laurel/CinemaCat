using CinemaCat.Api.DTO;
using CinemaCat.Api.Extensions;
using CinemaCat.Api.Handlers.Movies.CreateMovie;
using CinemaCat.Api.Handlers.Movies.DeleteMovie;
using CinemaCat.Api.Handlers.Movies.GetMovie;
using CinemaCat.Api.Handlers.Movies.SearchMovie;
using CinemaCat.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{movie_id}")]
    public async Task<ActionResult<Movie>> Get(Guid movie_id)
    {
        var req = new GetMovieRequest { Id = movie_id };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieModel movie)
    {
        var req = new CreateMovieRequest
        {
            Title = movie.Title,
            ReleasedDate = movie.ReleasedDate,
            Director = movie.DirectorName,
            Rating = movie.Rating
        };
        var response = await mediator.Send(req);
        return response.ToResult();
    }

    [HttpDelete]
    [Route("{movie_id}")]
    public async Task<ActionResult> Delete(Guid movie_id)
    {
        var req = new DeleteMovieRequest { Id = movie_id };
        var response = await mediator.Send(req);
        return response.ToResult();
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<List<Movie>>> Search(string title)
    {
        var req = new SearchMovieRequest { Title = title };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }
}
