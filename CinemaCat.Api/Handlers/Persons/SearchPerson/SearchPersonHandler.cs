using CinemaCat.Api.Data;
using CinemaCat.Api.Models;

namespace CinemaCat.Api.Handlers.Persons.SearchPerson;

public class SearchPersonHandler(IDataBaseProvider<Person> personsProvider)
    : ApplicationHandlerBase<SearchPersonPequest, SearchPersonResponse>
{
    protected override async Task<SearchPersonResponse> HandleInternal(
        SearchPersonPequest request,
        CancellationToken cancellationToken)
    {
        var result = await personsProvider.GetAsync(person => person.Name.Contains(request.Name));
        if (result.Count == 0)
            return new SearchPersonResponse { Error = "No artist with such name" };
        return new SearchPersonResponse { Result = result };
    }
}
