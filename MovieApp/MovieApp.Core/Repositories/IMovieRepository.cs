﻿using MovieApp.Core.Entities;

namespace MovieApp.Core.Repositories;
public interface IMovieRepository : IGenericRepository<Movie>
{
    Task<int> GetTotalMovieCountAsync();
    Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int count);
    Task<IEnumerable<Movie>> GetMostWatchedMoviesAsync(int count);
    Task<IEnumerable<Recommendation>> GetRecommendationsAsync(string userId);
    Task<IEnumerable<Movie>> GetPopularMoviesAsync();
    Task<IEnumerable<Movie>> GetRecentlyAddedMoviesAsync();
}

