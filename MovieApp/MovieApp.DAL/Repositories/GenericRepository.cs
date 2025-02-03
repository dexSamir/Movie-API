using System;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
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

    public IQueryable<T> GetAll(params string[] includes)
    {
        var query = Table.AsQueryable();
        foreach (var include in includes)
            query = query.Include(include);
        return query;
    }

    public async Task<T?> GetByIdAsync(int id)
        => await Table.FindAsync(id);

    public async Task<T?> GetByFilter(Expression<Func<T, bool>> expression)
        => await Table.FirstOrDefaultAsync(expression);

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);

    public async Task<bool> IsExistAsync(int id)
        => await Table.AnyAsync(x => x.Id == id);

    public async Task<bool> RemoveAsync(int id)
    {
        int result = await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
        return result > 0; 
    }

    public void Remove(T entity)
    {
        Table.Remove(entity); 
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

    public async Task<int> GetFilteredCount(Expression<Func<T, bool>> expression)
        => await Table.Where(expression).CountAsync(); 

    public async Task<ICollection<T>> GetFilteredList(Expression<Func<T, bool>> expression)
        => await Table.Where(expression).ToListAsync(); 
}

