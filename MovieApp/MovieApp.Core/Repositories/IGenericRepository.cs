using System.Linq.Expressions;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity, new() 
{
    Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true, params string[] includes);
    Task<IEnumerable<T>> GetAllAsync(params string[] includes);
    Task<int> CountAsync(int[] ids);

    Task<T?> GetByIdAsync(int id, bool asNoTrack = true, params string[] includes);
    Task<IEnumerable<T>> GetByIdsAsync(int[] ids, bool asNoTrack = true, params string[] includes);
    Task<T?> GetByIdAsync(int id, params string[] includes);

    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes);
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<bool> IsExistAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task ReverseSoftDeleteAsync(int id);
    Task DeleteRangeAsync(params int[] ids);
    Task SoftDeleteRangeAsync(params int[] ids);
    Task ReverseSoftDeleteRangeAsync(params int[] ids);

    void DeleteRange(params T[] entities);
    void SoftDeleteRange(params T[] entities);
    void ReverseSoftDeleteRange(params T[] entities);
    void Delete(T entity);
    void SoftDelete(T entity);
    void ReverseSoftDelete(T entity);

    Task DeleteAndSaveAsync(int id);
    Task<int> SaveAsync();
    //string GetCurrentUserId();
    //Task<User?> GetCurrentUserAsync();
}

