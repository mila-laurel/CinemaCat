using CinemaCat.Api.DTO;
using CinemaCat.Api.Extensions;
using CinemaCat.Api.Handlers.Persons.CreatePerson;
using CinemaCat.Api.Handlers.Persons.DeletePerson;
using CinemaCat.Api.Handlers.Persons.GetPerson;
using CinemaCat.Api.Handlers.Persons.SearchPerson;
using CinemaCat.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{person_id}")]
    public async Task<ActionResult<PersonDetails>> Get(Guid person_id)
    {
        var req = new GetPersonRequest { Id = person_id };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }

    [HttpPost]
    public async Task<ActionResult<PersonDetails>> Create([FromBody] CreatePersonModel person)
    {
        var req = new CreatePersonRequest
        {
            Name = person.Name,
            DateOfBirth = person.DateOfBirth,
            PlaceOfBirth = person.PlaceOfBirth
        };
        var response = await mediator.Send(req);
        return response.ToResult();
    }

    [HttpDelete]
    [Route("{person_id}")]
    public async Task<ActionResult> Delete(Guid person_id)
    {
        var req = new DeletePersonRequest { Id = person_id };
        var response = await mediator.Send(req);
        return response.ToResult();
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<List<PersonDetails>>> Search(string name)
    {
        var req = new SearchPersonPequest { Name = name };
        var response = await mediator.Send(req);
        return response.ToResult(r => Ok(r), e => NotFound(e));
    }
}
