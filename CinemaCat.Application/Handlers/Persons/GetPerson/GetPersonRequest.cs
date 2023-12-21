using MediatR;

namespace CinemaCat.Application.Handlers.Persons.GetPerson;

public class GetPersonRequest : IRequest<GetPersonResponse>
{
    public Guid Id { get; init; }
}
