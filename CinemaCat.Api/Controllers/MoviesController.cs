using CinemaCat.Api.DTO;
using CinemaCat.Api.Extensions;
using CinemaCat.Application.Handlers.Movies.CreateMovie;
using CinemaCat.Application.Handlers.Movies.DeleteMovie;
using CinemaCat.Application.Handlers.Movies.GetMovie;
using CinemaCat.Application.Handlers.Movies.SearchMovie;
using CinemaCat.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{movie_id}", Name = "GetMovie")]
    [ProducesResponseType(typeof(Movie), 200)]
    public async Task<ActionResult<Movie>> Get(Guid movie_id)
    {
        var req = new GetMovieRequest { Id = movie_id };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Movie), 201)]
    public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieModel movie)
    {
        var req = new CreateMovieRequest
        {
            Title = movie.Title,
            ReleasedDate = movie.ReleasedDate,
            Director = movie.Director,
            Rating = movie.Rating,
            TopActors = movie.TopActors,
            Poster = movie.Poster

        };
        var response = await mediator.Send(req);
        var url = Url.RouteUrl("GetMovie", new { movie_id = response.Result?.Id }, protocol: Request.Scheme);
        return response.ToResult(r => Created(url, r), e => NotFound(e));
    }

    [HttpDelete]
    [Route("{movie_id}")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<object>> Delete(Guid movie_id)
    {
        var req = new DeleteMovieRequest { Id = movie_id };
        var response = await mediator.Send(req);
        return response.ToResult();
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(List<Movie>), 200)]
    public async Task<ActionResult<List<Movie>>> Search(string title)
    {
        var req = new SearchMovieRequest { Title = title };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }
}
