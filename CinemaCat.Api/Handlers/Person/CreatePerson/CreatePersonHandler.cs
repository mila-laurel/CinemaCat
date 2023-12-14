using CinemaCat.Api.Data;
using CinemaCat.Api.Models;
using MediatR;

namespace CinemaCat.Api.Handlers.Person.CreatePerson;

public class CreatePersonHandler(IDataBaseProvider<PersonDetails> personsProvider) : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
{
    public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var newValue = new PersonDetails
        {
            Person = request.Name,
            DateOfBirth = DateOnly.Parse(request.DateOfBirth),
            PlaceOfBirth = request.PlaceOfBirth
        };
        var details = await personsProvider.CreateAsync(newValue);
        var response = new CreatePersonResponse
        {
            Id = details.Id,
            Person = details.Person.Name,
            DateOfBirth = details.DateOfBirth,
            PlaceOfBirth = details.PlaceOfBirth,
            Photo = details.Photo
        };

        return response;
    }
}
