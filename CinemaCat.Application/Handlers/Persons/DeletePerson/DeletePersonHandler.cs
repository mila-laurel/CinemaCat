using CinemaCat.Infrastructure.Data;
using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Application.Handlers.Persons.DeletePerson;

public class DeletePersonHandler(IDataBaseProvider<PersonDetails> personsProvider)
    : ApplicationHandlerBase<DeletePersonRequest, DeletePersonResponse>
{
    protected override async Task<DeletePersonResponse> HandleInternal(
        DeletePersonRequest request,
        CancellationToken cancellationToken)
    {
        await personsProvider.RemoveAsync(request.Id);
        return new DeletePersonResponse();
    }
}
