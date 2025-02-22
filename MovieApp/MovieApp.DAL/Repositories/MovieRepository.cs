using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
    readonly AppDbContext _context;
    public MovieRepository(AppDbContext context) : base(context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Movie>> GetMostWatchedMoviesAsync(int count)
    {

        return await _context.Movies
            .OrderByDescending(m => m.WatchCount)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int count)
    {
        return await _context.Movies
            .OrderByDescending(m => m.AvgRating)
            .Take(count).ToListAsync();
    }

    public async Task<int> GetTotalMovieCountAsync()
    {
        return await _context.Movies.CountAsync(); 
    }

    public async Task<IEnumerable<Recommendation>> GetRecommendationsAsync(string userId)
    {
        return await _context.Recommendations
            .Include(r => r.Movie) 
            .Include(r => r.Serie)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.RecommendedDate) 
            .Take(10) 
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
    {
        return await _context.Movies
            .OrderByDescending(m => m.WatchCount)
            .Take(10) 
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetRecentlyAddedMoviesAsync()
    {
        return await _context.Movies
            .OrderByDescending(m => m.CreatedTime)
            .Take(10) 
            .ToListAsync();
    }
}

