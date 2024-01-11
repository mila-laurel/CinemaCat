using CinemaCat.Domain.Models;
using MediatR;

namespace CinemaCat.Application.Handlers.Persons.CreatePerson;

public class CreatePersonRequest : IRequest<CreatePersonResponse>
{
    public required string Name { get; init; }
    public string? DateOfBirth { get; init; }
    public string? PlaceOfBirth { get; init; }
    public ProfileImage? Photo { get; init; }
}
