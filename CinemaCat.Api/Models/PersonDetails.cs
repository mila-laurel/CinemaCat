using System.Text.Json.Serialization;

namespace CinemaCat.Api.Models
{
    public class PersonDetails
    {
        public Guid Id { get; init; }

        public required Person Person { get; init; }

        public DateOnly DateOfBirth { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Country PlaceOfBirth { get; set; }
    }
}
