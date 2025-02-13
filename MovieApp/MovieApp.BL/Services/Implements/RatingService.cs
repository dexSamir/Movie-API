using MovieApp.BL.Exceptions.Rating;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class RatingService : IRatingService
{
    readonly ICurrentUser _user;
    readonly IRatingRepository _repo;
    public RatingService(IRatingRepository repo, ICurrentUser user)
    {
        _user = user;
        _repo = repo; 
    }

    public async Task<bool> RateMovieAsync(int movieId, int score)
    {
        var userId = _user.GetId();
        var existingRating = await _repo.GetFirstAsync(r => r.MovieId == movieId && r.UserId == userId);
        if (existingRating != null)
            throw new AlreadyRatedException();

        var rating = new Rating
        {
            Score = score,
            UserId = userId,
            MovieId = movieId
        };
        await _repo.AddAsync(rating);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> RateEpisodeAsync(int episodeId, int score)
    {
        var userId = _user.GetId();
        var existingRating = await _repo.GetFirstAsync(r => r.EpisodeId == episodeId && r.UserId == userId);
        if (existingRating != null)
            throw new AlreadyRatedException();

        var rating = new Rating
        {
            Score = score,
            UserId = userId,
            EpisodeId = episodeId
        };
        await _repo.AddAsync(rating);
        return await _repo.SaveAsync() > 0;
    }

    public Task<bool> RateSerieAsync(int serieId, int score)
    {
        throw new NotImplementedException();
    }


    public Task<double> GetAverageRatingForEpisodeAsync(int episodeId)
    {
        throw new NotImplementedException();
    }

    public Task<double> GetAverageRatingForMovieAsync(int movieId)
    {
        throw new NotImplementedException();
    }

    public Task<double> GetAverageRatingForSerieAsync(int serieId)
    {
        throw new NotImplementedException();
    }

}

