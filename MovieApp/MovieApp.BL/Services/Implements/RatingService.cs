using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
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
    readonly IEpisodeRepository _episodeRepository;

    public RatingService(IRatingRepository repo, ICurrentUser user, IEpisodeRepository episodeRepository)
    {
        _episodeRepository = episodeRepository;
        _user = user;
        _repo = repo; 
    }

    public async Task<bool> RateMovieAsync(int movieId, int score)
    {
        var userId = _user.GetId();
        await ValidateUserAsync(userId);

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
        await ValidateUserAsync(userId);

        if (score < 1 || score > 10)
            throw new InvalidScoreException();

        var episode = await _episodeRepository.GetByIdAsync(episodeId);
        if (episode == null)
            throw new NotFoundException<Episode>();

        if (episode.Season == null || episode.Season.SerieId == null)
            throw new InvalidEpisodeAssociationException();

        var serieId = episode.Season.SerieId;

        var existingRating = await _repo.GetFirstAsync(r => r.EpisodeId == episodeId && r.UserId == userId);
        if (existingRating != null)
        {
            existingRating.Score = score;
            return await _repo.SaveAsync() > 0;
        }

        var rating = new Rating
        {
            Score = score,
            UserId = userId,
            EpisodeId = episodeId,
            SerieId = serieId
        };

        await _repo.AddAsync(rating);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> RateSerieAsync(int serieId, int score)
    {
        var userId = _user.GetId();
        await ValidateUserAsync(userId);


        var existingRating = await _repo.GetFirstAsync(r => r.SerieId == serieId && r.UserId == userId);
        if (existingRating != null)
            throw new AlreadyRatedException();

        var rating = new Rating
        {
            Score = score,
            UserId = userId,
            SerieId = serieId
        };
        await _repo.AddAsync(rating);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> UpdateRatingAsync(int ratingId, int newScore)
    {
        var userId = _user.GetId();
        await ValidateUserAsync(userId);

        var rating = await _repo.GetByIdAsync(ratingId);
        if (rating == null) throw new NotFoundException<Rating>();

        if (rating.UserId != userId)
            throw new ForbiddenException<Rating>();

        if (newScore < 1 || newScore > 10)
            throw new InvalidScoreException();

        rating.Score = newScore;
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteRatingAsync(int ratingId)
    {
        var userId = _user.GetId();
        await ValidateUserAsync(userId);

        var rating = await _repo.GetByIdAsync(ratingId);
        if (rating == null) throw new NotFoundException<Rating>();

        if (rating.UserId != userId)
            throw new ForbiddenException<Rating>();

        _repo.Delete(rating);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<IEnumerable<Rating>> GetUserRatingsAsync()
    {
        var userId = _user.GetId();
        await ValidateUserAsync(userId);

        return await _repo.GetWhereAsync(r => r.UserId == userId);
    }


    public async Task<double> GetAverageRatingForEpisodeAsync(int episodeId)
    {
        var episodeRatings = await _repo.GetWhereAsync(r => r.EpisodeId == episodeId && r.EpisodeId == null);

        return episodeRatings.Any() ? episodeRatings.Average(r => r.Score) : 0;
    }

    public async Task<double> GetAverageRatingForMovieAsync(int movieId)
    {
        var movieRatings = await _repo.GetWhereAsync(r => r.MovieId == movieId && r.MovieId == null);

        return movieRatings.Any() ? movieRatings.Average(r => r.Score) : 0;

    }

    public async Task<double> GetAverageRatingForSerieAsync(int serieId)
    {
        var serieRatings = await _repo.GetWhereAsync(r => r.SerieId == serieId && r.EpisodeId == null);

        return serieRatings.Any() ? serieRatings.Average(r => r.Score) : 0;
    }

    public async Task<double> GetHybridRatingForSerieAsync(int serieId)
    {
        var ratings = await _repo.GetWhereAsync(r => r.SerieId == serieId);

        if (!ratings.Any())
            return 0; 

        var episodeRatings = ratings.Where(r => r.EpisodeId != null).ToList();
        var serieRatings = ratings.Where(r => r.EpisodeId == null).ToList();

        var averageEpisodeRating = episodeRatings.Any() ? episodeRatings.Average(r => r.Score) : 0;
        var averageSerieRating = serieRatings.Any() ? serieRatings.Average(r => r.Score) : 0;

        var hybridRating = (averageEpisodeRating * 0.7) + (averageSerieRating * 0.3);

        return hybridRating;
    }

    private async Task ValidateUserAsync(string userId)
    {
        if (userId == null)
            throw new AuthorisationException<User>();
    }
}

