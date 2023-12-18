using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CinemaCat.Infrastructure.Data;


// https://jira.mongodb.org/browse/CSHARP-3717
internal class DateOnlySerializer : StructSerializerBase<DateOnly>
{
    private const string DateOnlyFormat = "MM/dd/yyyy";

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
    {
        var dateTimeString = value.ToString(DateOnlyFormat);
        context.Writer.WriteString(dateTimeString);
    }

    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var dateTimeString = context.Reader.ReadString();
        return DateOnly.ParseExact(dateTimeString, DateOnlyFormat);
    }
}