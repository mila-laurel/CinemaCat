using System.Linq.Expressions;

namespace CinemaCat.Infrastructure.Data;

public interface IDataBaseProvider<T> where T : class
{
    Task<List<T?>> GetAsync();
    Task<T?> GetByIdAsync<TId>(TId id);
    Task<T> CreateAsync(T newValue);
    Task UpdateAsync<TId>(TId id, T newValue);
    Task RemoveAsync<TId>(TId id);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> filter);
}