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
    void UpdateAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    Task<bool> UpdatePropertyAsync(T entity, Expression<Func<T, object>> property, object value);

    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task ReverseSoftDeleteAsync(int id);
    Task DeleteRangeAsync(int[] ids);
    Task SoftDeleteRangeAsync(int[] ids);
    Task ReverseSoftDeleteRangeAsync(int[] ids);

    void DeleteRange(IEnumerable<T> entites);
    void SoftDeleteRange(IEnumerable<T> entites);
    void ReverseSoftDeleteRange(IEnumerable<T> entites);
    void Delete(T entity);
    void SoftDelete(T entity);
    void ReverseSoftDelete(T entity);

    Task DeleteAndSaveAsync(int id);
    Task<int> SaveAsync();

    Task<bool> HasUserReactedAsync(int entityId, string userId, bool isLike, Type entityType);
    Task AddUserReactionAsync(int entityId, string userId, bool isLike, Type entityType);
    Task RemoveUserReactionAsync(int entityId, string userId, bool isLike, Type entityType);
}

