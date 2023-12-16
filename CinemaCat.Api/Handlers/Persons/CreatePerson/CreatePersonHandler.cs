using CinemaCat.Api.Data;
using CinemaCat.Api.Models;

namespace CinemaCat.Api.Handlers.Persons.CreatePerson;

public class CreatePersonHandler(IDataBaseProvider<PersonDetails> personsProvider)
    : ApplicationHandlerBase<CreatePersonRequest, CreatePersonResponse>
{
    protected override async Task<CreatePersonResponse> HandleInternal(
        CreatePersonRequest request,
        CancellationToken cancellationToken)
    {
        var newValue = new PersonDetails
        {
            Name = request.Name,
            DateOfBirth = DateOnly.Parse(request.DateOfBirth),
            PlaceOfBirth = request.PlaceOfBirth
        };
        var details = await personsProvider.CreateAsync(newValue);
        return new CreatePersonResponse { Result = details };
    }
}
