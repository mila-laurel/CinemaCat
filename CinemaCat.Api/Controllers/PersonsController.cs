using CinemaCat.Api.Data;
using CinemaCat.Api.DTO;
using CinemaCat.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController(IDataBaseProvider<PersonDetails> personsProvider) : ControllerBase
    {
        [HttpGet]
        [Route("{person_id}")]
        public async Task<ActionResult<PersonDetails>> Get(Guid person_id)
        {
            var result = await personsProvider.GetByIdAsync(person_id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDetails>> Create([FromBody] CreatePersonModel person)
        {
            var newValue = new PersonDetails
            {
                Person = person.Name,
                DateOfBirth = DateOnly.Parse(person.DateOfBirth),
                PlaceOfBirth = person.PlaceOfBirth
            };

            return await personsProvider.CreateAsync(newValue);
        }

        [HttpDelete]
        [Route("{person_id}")]
        public async Task<IActionResult> Delete(Guid person_id)
        {
            await personsProvider.RemoveAsync(person_id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<PersonDetails>>> Search(string name)
        {
            return await personsProvider.GetAsync(p => p.Person.Name.Contains(name));
        }
    }
}
