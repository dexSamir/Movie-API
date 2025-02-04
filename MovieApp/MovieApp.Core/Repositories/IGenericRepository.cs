using System.Linq.Expressions;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity, new() 
{
    Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true, params string[] includes);
    Task<IEnumerable<T>> GetAllAsync(params string[] includes);

    Task<T?> GetByIdAsync(int id, bool asNoTrack = true, params string[] includes);
    Task<T?> GetByIdAsync(int id, params string[] includes);

    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes);
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<bool> IsExistAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(params T[] entities); 
    Task DeleteAsync(int id);
    Task DeleteAndSaveAsync(int id);
    void Delete(T entity);
    Task<int> SaveAsync();
    //string GetCurrentUserId();
    //Task<User?> GetCurrentUserAsync();
}

