using CinemaCat.Infrastructure.Models;
using MediatR;

namespace CinemaCat.Application.Handlers.Persons.CreatePerson;

public class CreatePersonRequest : IRequest<CreatePersonResponse>
{
    public string Name { get; init; }
    public string DateOfBirth { get; init; }
    public string PlaceOfBirth { get; init; }
    public ProfileImage? Photo { get; init; }
}
