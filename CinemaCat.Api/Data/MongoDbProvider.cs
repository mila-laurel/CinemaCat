using System.Linq.Expressions;
using MongoDB.Driver;

namespace CinemaCat.Api.Data;

public class MongoDbProvider<TEntity> : IDataBaseProvider<TEntity> where TEntity : class
{
    private const string IdPropertyName = "Id";
    private readonly IMongoCollection<TEntity?> _mongoCollection;

    public MongoDbProvider(IMongoDatabase mongoDatabase)
    {
        _mongoCollection = mongoDatabase.GetCollection<TEntity?>(GetCollectionName());
    }

    internal static string GetCollectionName()
    {
        var modelName = typeof(TEntity).Name;
        return modelName.EndsWith('s') ? modelName : modelName + "s";
    }

    public Task<List<TEntity?>> GetAsync() =>
        _mongoCollection.Find(_ => true).ToListAsync();

    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter) =>
        _mongoCollection!.Find(filter).ToListAsync();

    public Task<TEntity?> GetByIdAsync<TId>(TId id) =>
        _mongoCollection.Find(BuildIdEqualsExpression(id)).FirstOrDefaultAsync();

    public async Task<TEntity> CreateAsync(TEntity newValue)
    {
        await _mongoCollection.InsertOneAsync(newValue);
        return newValue;
    }

    public Task UpdateAsync<TId>(TId id, TEntity newValue) =>
        _mongoCollection.ReplaceOneAsync(BuildIdEqualsExpression(id), newValue);

    public Task RemoveAsync<TId>(TId id) =>
        _mongoCollection.DeleteOneAsync(BuildIdEqualsExpression(id));

    Expression<Func<TEntity?, bool>> BuildIdEqualsExpression<TId>(TId id)
    {
        // x => x.Id == id
        var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
        var idPropertyExpression = Expression.Property(parameterExpression, IdPropertyName);
        var idParameterExpression = Expression.Constant(id);
        var body = Expression.Equal(idPropertyExpression, idParameterExpression);
        return Expression.Lambda<Func<TEntity?, bool>>(body, parameterExpression);
    }
}