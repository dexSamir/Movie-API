using AutoMapper;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;

public class DirectorService : IDirectorService
{
    readonly IMapper _mapper;
    readonly IDirectorRepository _repo;
    readonly ICacheService _cache;
    readonly IFileService _fileService;

    const string defaultImage = "imgs/directors/default.png";
    const string CacheKeyPrefix = "director_";
    const string AllDirectorsCacheKey = "all_directors";

    public DirectorService(IDirectorRepository repo, IMapper mapper, IFileService fileService, ICacheService cache)
    {
        _cache = cache;
        _fileService = fileService;
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<IEnumerable<DirectorGetDto>> GetAllAsync()
    {
        var cachedData = await _cache.GetOrSetAsync(AllDirectorsCacheKey, async () =>
        {
            var directors = await _repo.GetAllAsync("Movies", "Series");
            var datas = _mapper.Map<IEnumerable<DirectorGetDto>>(directors);

            foreach (var dto in datas)
            {
                var director = directors.FirstOrDefault(d => d.Id == dto.Id);
                if (director != null)
                {
                    dto.MoviesCount = director.Movies?.Count ?? 0;
                    dto.SeriesCount = director.Series?.Count ?? 0;
                }
            }

            return datas;
        }, TimeSpan.FromMinutes(10));

        return cachedData;
    }

    public async Task<DirectorGetDto> GetByIdAsync(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var director = await _repo.GetByIdAsync(id, "Movies", "Series");
            if (director == null)
                throw new NotFoundException<Director>();

            var data = _mapper.Map<DirectorGetDto>(director);
            data.MoviesCount = director.Movies?.Count ?? 0;
            data.SeriesCount = director.Series?.Count ?? 0;
            return data;
        }, TimeSpan.FromMinutes(10));

        return cachedData;
    }

    public async Task<int> CreateAsync(DirectorCreateDto dto)
    {
        var director = _mapper.Map<Director>(dto);
        director.CreatedTime = DateTime.UtcNow;
        director.ImageUrl = dto.ImageUrl == null || dto.ImageUrl.Length == 0
            ? defaultImage
            : await _fileService.ProcessImageAsync(dto.ImageUrl, "directors", "image/" , 15);

        await _repo.AddAsync(director);
        await _repo.SaveAsync();

        await _cache.RemoveAsync(AllDirectorsCacheKey);

        return director.Id;
    }

    public async Task<bool> UpdateAsync(DirectorUpdateDto dto, int id)
    {
        var director = await _repo.GetByIdAsync(id, false);
        if (director == null)
            throw new NotFoundException<Director>();

        _mapper.Map(dto, director);

        if (dto.ImageUrl != null)
        {
            await _fileService.DeleteImageIfNotDefault(director.ImageUrl, "directors");
            director.ImageUrl = await _fileService.ProcessImageAsync(dto.ImageUrl, "directors", "image/", 15);
        }

        director.UpdatedTime = DateTime.UtcNow;
        bool isUpdated = await _repo.SaveAsync() > 0;

        if (isUpdated)
            await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

        return isUpdated;
    }

    public async Task<bool> DeleteAsync(string ids, EDeleteType deleteType)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureDirectorExist(idArray);

        foreach (var id in idArray)
        {
            var director = await _repo.GetByIdAsync(id, false);
            if (director != null)
            {
                if (deleteType == EDeleteType.Hard)
                    await _fileService.DeleteImageIfNotDefault(director.ImageUrl, "directors");

                await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");
            }
        }

        bool success = false;
        switch (deleteType)
        {
            case EDeleteType.Soft:
                await _repo.SoftDeleteRangeAsync(idArray);
                success = true;
                break;
            case EDeleteType.Hard:
                await _repo.DeleteRangeAsync(idArray);
                success = true;
                break;
            case EDeleteType.Reverse:
                await _repo.ReverseSoftDeleteRangeAsync(idArray);
                success = true;
                break;
        }

        if (success)
            await _cache.RemoveAsync(AllDirectorsCacheKey);

        return success;
    }

    private async Task EnsureDirectorExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Director>();
    }
}
