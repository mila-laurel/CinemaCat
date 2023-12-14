using CinemaCat.Api.Data;
using CinemaCat.Api.DTO;
using CinemaCat.Api.Handlers.Person.CreatePerson;
using CinemaCat.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController(IDataBaseProvider<PersonDetails> personsProvider, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{person_id}")]
    public async Task<ActionResult<PersonDetails>> Get(Guid person_id)
    {
        var result = await personsProvider.GetByIdAsync(person_id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreatePersonResponse>> Create([FromBody] CreatePersonModel person)
    {
        var req = new CreatePersonRequest
        {
            Name = person.Name,
            DateOfBirth = person.DateOfBirth,
            PlaceOfBirth = person.PlaceOfBirth
        };
        return await mediator.Send(req);
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
