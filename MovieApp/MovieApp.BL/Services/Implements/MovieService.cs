using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class MovieService : IMovieService
{
    readonly IMovieRepository _repo;
    readonly IActorRepository _actRepo;
    readonly IMapper _mapper;
    private readonly IFileService _fileService;

    private readonly string[] _includeProperties =
    {
        "Actors", "MovieSubtitles", "Genres", "Ratings",
        "Reviews", "Rentals", "AudioTracks", "Recommendations"
    };

    public MovieService(IMovieRepository repo, IMapper mapper, IActorRepository actRepo, IFileService fileService)
    {
        _actRepo = actRepo;
        _fileService = fileService; 
        _mapper = mapper;
        _repo = repo;
    }

    // GET AND SORTINGS 
    public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
    }

    public async Task<MovieGetDto> GetByIdAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, _includeProperties);

        if (data == null)
            throw new NotFoundException<Movie>();
        return _mapper.Map<MovieGetDto>(data);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByGenre(string genre)
    {
        var datas = await _repo.
            GetWhereAsync(x =>
            x.Genres.Any(y => y.Genre.Name == genre), _includeProperties);

        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByRating(double rating)
    {
        var movies = await _repo.GetWhereAsync(x => x.AvgRating > rating, _includeProperties);
        var filteredMovies = movies.Where(m => m.Ratings.Any() ? m.Ratings.Average(r => r.Score) >= rating : false);
        if (!filteredMovies.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(filteredMovies);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByReleaseDateAsync(DateOnly releaseDate)
    {
        var datas = await _repo.GetWhereAsync(x => x.ReleaseDate >= releaseDate, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByDirectorAsync(int directorId)
    {
        var datas = await _repo.GetWhereAsync(x => x.DirectorId == directorId, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByActorAsync(int actorId)
    {
        var datas = await _repo.GetWhereAsync(x => x.Actors.Any(y => y.ActorId == actorId), _includeProperties);

        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByDurationRangeAsync(int minDuration, int maxDuration)
    {
        var datas = await _repo.GetWhereAsync(x => x.Duration >= minDuration && x.Duration <= maxDuration, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();
        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByTitleAsync(string title)
    {
        var datas = await _repo.GetWhereAsync(x => x.Title.Contains(title), _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();
        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> SortByTitleAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.Title) :
            movies.OrderByDescending(x => x.Title);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies); 
    }

    public async Task<IEnumerable<MovieGetDto>> SortByReleaseDateAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.ReleaseDate) :
            movies.OrderByDescending(x => x.ReleaseDate);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);
    }

    public async Task<IEnumerable<MovieGetDto>> SortByRatingAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.AvgRating) :
            movies.OrderByDescending(x => x.AvgRating);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);
    }

    public async Task<double> GetAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Actors", "MovieSubtitles", "Genres", "AudioTracks");
        if (movie == null)
            throw new NotFoundException<Movie>();

        return movie.AvgRating; 
    }


    //UPDATE AND CREATES
    public async Task<bool> UpdateAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Ratings");

        if (movie == null)
            throw new NotFoundException<Movie>();

        var newRating = movie.AvgRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;

        return await _repo.UpdatePropertyAsync(movie, x => x.AvgRating, newRating);
    }

    public async Task<int> CreateAsync(MovieCreateDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);

        movie.Actors = dto.ActorIds.ToMovieActors();
        movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
        movie.Genres = dto.GenreIds.ToMovieGenres();
        movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();

        await _repo.AddAsync(movie);
        await _repo.SaveAsync();

        return movie.Id;
    }

    public async Task<IEnumerable<int>> CreateRangeAsync(IEnumerable<MovieCreateDto> dtos)
    {
        if (dtos == null || !dtos.Any())
            throw new ArgumentException("Movie list cannot be empty.", nameof(dtos));

        var movies = _mapper.Map<IEnumerable<Movie>>(dtos);

        foreach (var (dto, movie) in dtos.Zip(movies))
        {
            movie.Actors = dto.ActorIds.ToMovieActors();
            movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
            movie.Genres = dto.GenreIds.ToMovieGenres();
            movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();
        }

        await _repo.AddRangeAsync(movies);
        await _repo.SaveAsync();

        return movies.Select(m => m.Id);
    }

    public async Task<bool> UpdateAsync(MovieUpdateDto dto, int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Actors", "MovieSubtitles", "Genres", "AudioTracks");
        if (movie == null)
            throw new NotFoundException<Movie>();

        _mapper.Map(dto, movie);


        movie.Actors = dto.ActorIds.ToMovieActors();
        movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
        movie.Genres = dto.GenreIds.ToMovieGenres();
        movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();

        movie.PosterUrl = await _fileService.ProcessImageAsync(dto.PosterFile, "movies/posters", "image/", 50, movie.PosterUrl);
        movie.TrailerUrl = await _fileService.ProcessImageAsync(dto.TrailerFile, "movies/trailers", "video/", 1024, movie.TrailerUrl);

        _repo.UpdateAsync(movie);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> UpdatePosterUrlAsync(int movieId, IFormFile posterFile)
    {
        var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        movie.PosterUrl = await _fileService.ProcessImageAsync(posterFile, "movies/posters", "image/", 15, movie.PosterUrl);

        _repo.UpdateAsync(movie);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> UpdateTrailerUrlAsync(int movieId, IFormFile trailerFile)
    {
        var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        movie.TrailerUrl = await _fileService.ProcessImageAsync(trailerFile, "movies/trailers", "video/", 1024, movie.TrailerUrl);

        _repo.UpdateAsync(movie);
        return await _repo.SaveAsync() > 0;
    }


    //DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Movie>();

        await _fileService.DeleteImageIfNotDefault(data.PosterUrl, "movies/posters");
        await _fileService.DeleteImageIfNotDefault(data.TrailerUrl, "movies/trailers");

        await _repo.DeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureMovieExist(idArray);

        foreach (var id in idArray)
        {
            var data = await _repo.GetByIdAsync(id, false);
            if (data != null)
            {
                await _fileService.DeleteImageIfNotDefault(data.TrailerUrl, "movies/trailers");
                await _fileService.DeleteImageIfNotDefault(data.PosterUrl, "movies/posters");
            }
        }
        await _repo.DeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await EnsureMovieExists(id);

        await _repo.ReverseSoftDeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureMovieExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            throw new NotFoundException<Movie>();

        _repo.SoftDelete(data);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureMovieExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }


    private async Task EnsureMovieExists(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Movie>();
    }

    private async Task EnsureMovieExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Movie>();
    }
}

