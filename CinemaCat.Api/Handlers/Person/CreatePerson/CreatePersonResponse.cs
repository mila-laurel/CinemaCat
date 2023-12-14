using CinemaCat.Api.Models;

namespace CinemaCat.Api.Handlers.Person.CreatePerson;

public class CreatePersonResponse
{
    public Guid Id { get; init; }

    public string Person { get; init; }

    public DateOnly DateOfBirth { get; set; }

    public string PlaceOfBirth { get; set; }

    public Image Photo { get; set; }
}
