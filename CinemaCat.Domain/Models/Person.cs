namespace CinemaCat.Domain.Models;

public record class Person(string Name, Guid? PersonDetails, ProfileImage? PhotoUrl);