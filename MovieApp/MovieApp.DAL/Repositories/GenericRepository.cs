using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities.Base;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new() 
{
    readonly AppDbContext _context;
    protected DbSet<T> Table => _context.Set<T>();

    public GenericRepository(AppDbContext context)
    {
        _context = context; 
    }

    public async Task AddAsync(T entity)
        => await Table.AddAsync(entity); 

    public async Task AddRangeAsync(params T[] entities)
        => await Table.AddRangeAsync(entities);

    public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
    {
        return await GetAllAsync(true, includes);
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true, params string[] includes)
    {
        return await _includeAndTracking(Table, asNoTrack, includes).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, bool asNoTrack = true, params string[] includes)
    {
        return await _includeAndTracking(Table, asNoTrack, includes).FirstOrDefaultAsync(x => x.Id == id); 
    }

    public async Task<T?> GetByFilter(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes)
    {
        return await _includeAndTracking(Table, asNoTrack, includes).FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes)
    {
        return await _includeAndTracking(Table.Where(expression), asNoTrack, includes).ToListAsync();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes)
    {
        return await _includeAndTracking(Table.Where(expression), asNoTrack, includes).FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);

    public async Task<bool> IsExistAsync(int id)
        => await Table.AnyAsync(x => x.Id == id);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        Table.Remove(entity!);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity); 
    }

    public async Task DeleteAndSaveAsync(int id)
    {
        await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

    //public string GetCurrentUserId()
    //    => _http.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

    //public async Task<User?> GetCurrentUserAsync()
    //{
    //    string userId = GetCurrentUserId();
    //    if (string.IsNullOrWhiteSpace(userId))
    //        return null;
    //    return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId); 
    //}

    IQueryable<T> _includeAndTracking(IQueryable<T> query, bool asNoTrack, params string[] includes)
    {
        if (includes is not null && includes.Length > 0)
        {
            query = _checkIncludes(query, includes);
            if (asNoTrack)
                query = query.AsNoTrackingWithIdentityResolution();
        }
        else
            if (asNoTrack)
                query = query.AsNoTracking();

        return query; 
    }

    IQueryable<T> _checkIncludes(IQueryable<T> query, params string[] includes)
    {
            foreach (var include in includes)
                query = query.Include(include);

        return query; 
    }

}

