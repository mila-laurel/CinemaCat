namespace CinemaCat.Infrastructure.Models;

public record class Person(string Name, Guid? PersonDetails, ProfileImage? PhotoUrl);