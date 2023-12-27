namespace CinemaCat.Api.DTO;

public record class CreateUserModel(string Name, string Email, string[]? Roles, string Password) { }
