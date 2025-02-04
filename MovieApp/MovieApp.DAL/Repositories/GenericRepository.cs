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
        => await GetAllAsync(true, includes);

    public async Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true, params string[] includes)
        => await _includeAndTracking(Table, asNoTrack, includes).ToListAsync();

    public async Task<T?> GetByIdAsync(int id, bool asNoTrack = true, params string[] includes)
        => await _includeAndTracking(Table, asNoTrack, includes).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T?> GetByIdAsync(int id, params string[] includes)
        => await GetByIdAsync(id, true, includes);

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes)
        => await _includeAndTracking(Table.Where(expression), asNoTrack, includes).ToListAsync();

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[] includes)
        => await GetWhereAsync(expression, true, includes);

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[] includes)
        => await _includeAndTracking(Table.Where(expression), asNoTrack, includes).FirstOrDefaultAsync();

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, params string[] includes)
        => await GetFirstAsync(expression, true, includes);

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);

    public async Task<bool> IsExistAsync(int id)
        => await Table.AnyAsync(x => x.Id == id);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id, false);
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

    public async Task SoftDeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id, false);
        entity!.IsDeleted = true; 
    }

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true; 
    }
    public async Task ReverseSoftDeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id, false);
        entity!.IsDeleted = false;
    }

    public void ReverseSoftDelete(T entity)
    {
        entity.IsDeleted = false;
    }

    public async Task DeleteRangeAsync(params int[] ids)
    {
        var entities = await Table.Where(x => ids.Contains(x.Id)).ToListAsync();
        if (entities.Any()) Table.RemoveRange(entities);
    }

    public async Task SoftDeleteRangeAsync(params int[] ids)
    {
        var entities = await Table.Where(x => ids.Contains(x.Id)).ToListAsync();
        if (entities.Any())
        {
            foreach (var entity in entities) entity.IsDeleted = true;
            Table.UpdateRange(entities);
        }
    }

    public async Task ReverseSoftDeleteRangeAsync(params int[] ids)
    {
        var entities = await Table.Where(x => ids.Contains(x.Id)).ToListAsync();
        foreach (var entity in entities) entity.IsDeleted = false;
        Table.UpdateRange(entities);
    }

    public void DeleteRange(params T[] entities)
    {
        Table.RemoveRange(entities); 
    }

    public void SoftDeleteRange(params T[] entities)
    {
        foreach (var entity in entities) entity.IsDeleted = true;
        Table.UpdateRange(entities);
    }

    public void ReverseSoftDeleteRange(params T[] entities)
    {
        foreach (var entity in entities) entity.IsDeleted = false;
        Table.UpdateRange(entities);
    }
}

