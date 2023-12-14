namespace CinemaCat.Api.Models;

public record class Person(string Name, Guid? PersonDetails, Image PhotoUrl);