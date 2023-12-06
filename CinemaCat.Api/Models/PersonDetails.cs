using System.Globalization;

namespace CinemaCat.Api.Models
{
    public class PersonDetails
    {
        public Guid Id { get; init; }
        public required Person Person { get; init; }
        public DateOnly DateOfBirth { get; set; }
        public RegionInfo PlaceOfBirth { get; set; }
    }
}
