using MediatR;

namespace CinemaCat.Api.Handlers.Person.CreatePerson;

public class CreatePersonRequest : IRequest<CreatePersonResponse>
{
    public Models.Person Name { get; internal set; }
    public string DateOfBirth { get; internal set; }
    public string PlaceOfBirth { get; internal set; }
}
