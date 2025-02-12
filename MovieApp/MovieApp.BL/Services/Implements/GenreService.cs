using AutoMapper;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

public class GenreService : IGenreService
{
    readonly IGenreRepository _repo;
    readonly IMapper _mapper;
    readonly ICacheService _cache;
    public GenreService(IGenreRepository repo, IMapper mapper, ICacheService cache)
    {
        _mapper = mapper;
        _cache = cache;
        _repo = repo;
    }

    public async Task<IEnumerable<GenreGetDto>> GetAllAsync()
    {
        var cacheKey = "all_genres";
        var genres = await _cache.GetOrSetAsync(cacheKey, async () => await _repo.GetAllAsync(), TimeSpan.FromMinutes(5));

        var datas = _mapper.Map<IEnumerable<GenreGetDto>>(genres);

        foreach (var dto in datas)
        {
            var genre = genres.FirstOrDefault(x => x.Id == dto.Id);
            if (genre != null)
            {
                dto.MovieCount = genre.Movies?.Count ?? 0;
                dto.SerieCount = genre.Series?.Count ?? 0;
            }
        }

        return datas;
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        var cacheKey = $"genre_{id}";
        var genre = await _cache.GetOrSetAsync(cacheKey, async () => await _repo.GetByIdAsync(id), TimeSpan.FromMinutes(5));

        if (genre == null)
            throw new NotFoundException<Genre>();

        var data = _mapper.Map<GenreGetDto>(genre);
        data.SerieCount = genre.Series?.Count ?? 0;
        data.MovieCount = genre.Movies?.Count ?? 0;
        return data;
    }

    public async Task<int> CreateAsync(GenreCreateDto dto)
    {
        var data = _mapper.Map<Genre>(dto);
        data.CreatedTime = DateTime.UtcNow;

        await _repo.AddAsync(data);
        await _repo.SaveAsync();

        await _cache.RemoveAsync("all_genres");

        return data.Id;
    }

    public async Task CreateRangeAsync(IEnumerable<GenreCreateDto> dtos)
    {
        var datas = _mapper.Map<IEnumerable<Genre>>(dtos);
        foreach (var data in datas)
            data.CreatedTime = DateTime.UtcNow;

        await _repo.AddRangeAsync(datas);
        await _repo.SaveAsync();

        await _cache.RemoveAsync("all_genres");
    }

    public async Task<bool> UpdateAsync(int id, GenreUpdateDto dto)
    {
        await EnsureGenreExists(id);
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Genre>();

        _mapper.Map(dto, data);
        data.UpdatedTime = DateTime.UtcNow;

        var success = await _repo.SaveAsync() > 0;

        if (success)
        {
            await _cache.RemoveAsync($"genre_{id}");

            await _cache.RemoveAsync("all_genres");
        }

        return success;
    }


    //DELETE
    public async Task<bool> DeleteAsync(string ids, EDeleteType deleteType)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureGenresExist(idArray);

        switch (deleteType)
        {
            case EDeleteType.Soft:
                await _repo.SoftDeleteRangeAsync(idArray);
                break;
            case EDeleteType.Hard:
                await _repo.DeleteRangeAsync(idArray);
                break;
            case EDeleteType.Reverse:
                await _repo.ReverseSoftDeleteRangeAsync(idArray);
                break;
        }

        bool success = idArray.Length == await _repo.SaveAsync();

        if (success)
        {
            foreach (var id in idArray)
                await _cache.RemoveAsync($"genre_{id}");

            await _cache.RemoveAsync("all_genres");
        }

        return success;
    }

    private async Task EnsureGenreExists(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Genre>();
    }

    private async Task EnsureGenresExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Genre>();
    }
}
