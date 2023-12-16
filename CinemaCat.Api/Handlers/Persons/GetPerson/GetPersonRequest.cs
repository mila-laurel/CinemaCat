using MediatR;

namespace CinemaCat.Api.Handlers.Persons.GetPerson;

public class GetPersonRequest : IRequest<GetPersonResponse>
{
    public Guid Id { get; init; }
}
