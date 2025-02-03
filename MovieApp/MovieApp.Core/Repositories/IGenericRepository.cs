using System.Linq.Expressions;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity, new() 
{
    IQueryable<T> GetAll(params string[] includes);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByFilter(Expression<Func<T, bool>> expression);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<bool> IsExistAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(params T[] entities); 
    Task<bool> RemoveAsync(int id);
    void Remove(T entity);
    Task<int> SaveAsync();
    //string GetCurrentUserId();
    //Task<User?> GetCurrentUserAsync();
    Task <int> GetFilteredCount(Expression<Func<T, bool>> expression); 
    Task<ICollection<T>> GetFilteredList(Expression<Func<T, bool>> expression); 
}

