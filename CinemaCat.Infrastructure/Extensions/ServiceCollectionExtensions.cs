using CinemaCat.Application.Interfaces;
using CinemaCat.Infrastructure.Configuration;
using CinemaCat.Infrastructure.Data;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CinemaCat.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        AddBlobStorage(serviceCollection, configuration);

        AddMongoDb(serviceCollection, configuration);
    }

    private static void AddMongoDb(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        ConfigureBsonTypesMapping();
        serviceCollection.Configure<MongoConfiguration>(configuration.GetSection(MongoConfiguration.SectionName));

        serviceCollection.AddSingleton<MongoClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<MongoConfiguration>>();
            return new MongoClient(configuration.GetConnectionString("MongoDB"));
        });
        serviceCollection.AddSingleton<IMongoDatabase>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<MongoConfiguration>>();
            var client = provider.GetRequiredService<MongoClient>();
            return client.GetDatabase(options.Value.DataBaseName);
        });

        serviceCollection.AddScoped(typeof(IDataBaseProvider<>), typeof(MongoDbProvider<>));
    }

    private static void AddBlobStorage(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorage") ?? throw new ArgumentNullException("Blob strorage connection string"), preferMsi: true);
        });
        serviceCollection.AddScoped(typeof(IBlobServiceProvider), typeof(AzureBlobProvider));
    }

    private static void ConfigureBsonTypesMapping()
    {
        BsonSerializer.RegisterSerializer(new DateOnlySerializer());
    }
}