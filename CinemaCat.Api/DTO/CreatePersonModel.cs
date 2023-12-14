using CinemaCat.Api.Models;

namespace CinemaCat.Api.DTO;

public record class CreatePersonModel(string Name, string DateOfBirth, string PlaceOfBirth);
