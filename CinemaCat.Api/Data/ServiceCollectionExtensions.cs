using CinemaCat.Api.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CinemaCat.Api.Data;

public static class ServiceCollectionExtensions
{
    public static void AddDataBase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoConfiguration>(configuration.GetSection(MongoConfiguration.SectionName));

        ConfigureBsonTypesMapping();
        
        serviceCollection.AddSingleton<MongoClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<MongoConfiguration>>();
            return new MongoClient(options.Value.ConnectionString);
        });
        serviceCollection.AddSingleton<IMongoDatabase>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<MongoConfiguration>>();
            var client = provider.GetRequiredService<MongoClient>();
            return client.GetDatabase(options.Value.DataBaseName);
        });
        
        serviceCollection.AddScoped(typeof(IDataBaseProvider<>), typeof(MongoDbProvider<>));
    }

    private static void ConfigureBsonTypesMapping()
    {
        BsonSerializer.RegisterSerializer(new DateOnlySerializer());
    }
}