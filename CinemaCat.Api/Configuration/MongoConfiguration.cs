namespace CinemaCat.Api.Configuration;

public class MongoConfiguration
{
    public const string SectionName = "MongoDB";
    
    public string ConnectionString { get; set; }
    public string DataBaseName { get; set; }
}