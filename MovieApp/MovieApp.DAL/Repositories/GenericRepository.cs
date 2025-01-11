using System;
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
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetCurrentUserAsync()
    {
        throw new NotImplementedException();
    }

    public int GetCurrentUserId()
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }
}

