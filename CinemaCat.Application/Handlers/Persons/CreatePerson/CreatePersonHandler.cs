using CinemaCat.Application.Interfaces;
using CinemaCat.Domain.Models;

namespace CinemaCat.Application.Handlers.Persons.CreatePerson;

public class CreatePersonHandler(IDataBaseProvider<PersonDetails> personsProvider)
    : ApplicationHandlerBase<CreatePersonRequest, CreatePersonResponse>
{
    protected override async Task<CreatePersonResponse> HandleInternalAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken)
    {
        var newValue = new PersonDetails
        {
            Name = request.Name,
            DateOfBirth = DateOnly.Parse(request.DateOfBirth ?? string.Empty),
            PlaceOfBirth = request.PlaceOfBirth,
            Photo = request.Photo
        };
        var details = await personsProvider.CreateAsync(newValue);
        return new CreatePersonResponse { Result = details };
    }
}
