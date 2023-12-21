using MediatR;

namespace CinemaCat.Application.Handlers.Persons.SearchPerson;

public class SearchPersonPequest : IRequest<SearchPersonResponse>
{
    public string Name { get; init; }
}
