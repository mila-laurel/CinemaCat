using CinemaCat.Infrastructure.Data;
using CinemaCat.Infrastructure.Models;

namespace CinemaCat.Application.Handlers.Persons.CreatePerson;

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
            PlaceOfBirth = request.PlaceOfBirth,
            Photo = request.Photo
        };
        var details = await personsProvider.CreateAsync(newValue);
        return new CreatePersonResponse { Result = details };
    }
}
