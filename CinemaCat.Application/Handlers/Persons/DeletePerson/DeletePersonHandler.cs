using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Handlers.Persons.DeletePerson;

public class DeletePersonHandler(IDataBaseProvider<PersonDetails> personsProvider)
    : ApplicationHandlerBase<DeletePersonRequest, DeletePersonResponse>
{
    protected override async Task<DeletePersonResponse> HandleInternalAsync(
        DeletePersonRequest request,
        CancellationToken cancellationToken)
    {
        await personsProvider.RemoveAsync(request.Id);
        return new DeletePersonResponse();
    }
}
