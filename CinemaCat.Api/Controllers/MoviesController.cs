using CinemaCat.Api.Data;
using CinemaCat.Api.DTO;
using CinemaCat.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IDataBaseProvider<Movie> moviesProvider) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Movie>> Get(Guid id)
        {
            var result = await moviesProvider.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieModel movie)
        {
            var newValue = new Movie
            {
                Title = movie.Title,
                Rating = movie.Rating,
                ReleasedDate = DateOnly.Parse(movie.ReleasedDate),
                Director = movie.Director,
                TopActors = []
            };
            return await moviesProvider.CreateAsync(newValue);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await moviesProvider.RemoveAsync(id);
            return Ok();
        }
        
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<Movie>>> Search(string title)
        {
            return await moviesProvider.GetAsync(movie => movie.Title.Contains(title));
        }
    }
}
