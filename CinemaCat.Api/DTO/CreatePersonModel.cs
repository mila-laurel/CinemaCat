using CinemaCat.Api.Models;

namespace CinemaCat.Api.DTO
{
    public record class CreatePersonModel(Person Name, string DateOfBirth, string PlaceOfBirth);
}
