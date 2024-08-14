namespace Api.Persistance.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task DeleteAsync(Guid id);
}
