namespace CinemaCat.Domain.Models;

public record class ApplicationUser
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string[]? Roles { get; set; }
    public string? PasswordHash { get; set; }
    public required string Salt { get; set; }
}
