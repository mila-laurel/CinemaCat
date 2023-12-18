using CinemaCat.Infrastructure.Data;
using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Application.Handlers.Persons.GetPerson;

public class GetPersonHandler(IDataBaseProvider<PersonDetails> personsProvider)
    : ApplicationHandlerBase<GetPersonRequest, GetPersonResponse>
{
    protected override async Task<GetPersonResponse> HandleInternal(
        GetPersonRequest request,
        CancellationToken cancellationToken)
    {
        var result = await personsProvider.GetByIdAsync(request.Id);
        if (result == null)
            return new GetPersonResponse { Error = "There is no items with such id" };
        return new GetPersonResponse { Result = result };
    }
}
