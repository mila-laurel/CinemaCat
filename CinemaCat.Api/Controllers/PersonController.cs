using CinemaCat.Api.Data;
using CinemaCat.Api.DTO;
using CinemaCat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CinemaCat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IDataBaseProvider<PersonDetails> personProvider) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PersonDetails>> Get(Guid id)
        {
            var result = await personProvider.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDetails>> Create([FromBody] CreatePersonModel person)
        {
            var newValue = new PersonDetails
            {
                Person = person.Name,
                DateOfBirth = DateOnly.Parse(person.DateOfBirth),
                PlaceOfBirth = new RegionInfo(person.PlaceOfBirth)
            };

            return await personProvider.CreateAsync(newValue);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await personProvider.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<PersonDetails>>> Search(string title)
        {
            return await personProvider.GetAsync(p => p.Person.Name.Contains(title));
        }
    }
}
