using MediatR;

namespace CinemaCat.Api.Handlers.Persons.SearchPerson;

public class SearchPersonPequest : IRequest<SearchPersonResponse>
{
    public string Name { get; init; }
}
