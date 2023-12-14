using MediatR;

namespace CinemaCat.Api.Handlers.Person.CreatePerson;

public class CreatePersonRequest : IRequest<CreatePersonResponse>
{
    public string Name { get; init; }
    public string DateOfBirth { get; init; }
    public string PlaceOfBirth { get; init; }
}
