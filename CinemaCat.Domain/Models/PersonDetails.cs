namespace CinemaCat.Domain.Models;

public record class PersonDetails
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public DateOnly DateOfBirth { get; set; }

    public string? PlaceOfBirth { get; set; }

    public ProfileImage? Photo { get; set; }
}
