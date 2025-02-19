using AutoMapper;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements; 
public class ActorService : IActorService
{

    readonly IMapper _mapper;
    readonly IActorRepository _repo;
    readonly ICacheService _cache;
    private readonly IFileService _fileService;
    public ActorService(IActorRepository repo, IMapper mapper, IFileService fileService, ICacheService cache)
    {
        _fileService = fileService;
        _cache = cache; 
        _mapper = mapper;
        _repo = repo;
    }

    private const string defaultImage = "imgs/actors/default.png";

    public async Task<IEnumerable<ActorGetDto>> GetAllAsync()
    {
        return await _cache.GetOrSetAsync("all_actors", async () =>
        {
            var actors = await _repo.GetAllAsync("Movies", "Series");
            return _mapper.Map<IEnumerable<ActorGetDto>>(actors).Select(dto =>
            {
                var actor = actors.FirstOrDefault(d => d.Id == dto.Id);
                if (actor != null)
                {
                    dto.MoviesCount = actor.Movies?.Count ?? 0;
                    dto.SeriesCount = actor.Series?.Count ?? 0;
                }
                return dto;
            });
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<ActorGetDto> GetByIdAsync(int id)
    {
        return await _cache.GetOrSetAsync($"actor_{id}", async () =>
        {
            var actor = await _repo.GetByIdAsync(id, "Movies", "Series");
            if (actor == null)
                throw new NotFoundException<Actor>();

            var data = _mapper.Map<ActorGetDto>(actor);
            data.SeriesCount = actor.Series?.Count ?? 0;
            data.MoviesCount = actor.Movies?.Count ?? 0;
            return data;
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<int> CreateAsync(ActorCreateDto dto)
    {
        var actor = _mapper.Map<Actor>(dto);
        actor.CreatedTime = DateTime.UtcNow;
        actor.ImageUrl = dto.ImageUrl == null || dto.ImageUrl.Length == 0
            ? defaultImage
            : await _fileService.ProcessImageAsync(dto.ImageUrl, "actors", "image/", 15);

        await _repo.AddAsync(actor);
        await _repo.SaveAsync();

        await _cache.RemoveAsync("all_actors");

        return actor.Id;
    }

    public async Task<bool> UpdateAsync(ActorUpdateDto dto, int id)
    {
        var data = await _repo.GetByIdAsync(id, false) ?? throw new NotFoundException<Actor>();
        _mapper.Map(dto, data);
        if (dto.ImageUrl != null)
            data.ImageUrl = await _fileService.ProcessImageAsync(dto.ImageUrl, "actors", "image/", 15, data.ImageUrl);

        data.UpdatedTime = DateTime.UtcNow;
        bool updated = await _repo.SaveAsync() > 0;

        if (updated)
            await RemoveActorCacheAsync(id);

        return updated;
    }

    private async Task RemoveActorCacheAsync(int id)
    {
        await _cache.RemoveAsync($"actor_{id}");
        await _cache.RemoveAsync("all_actors");
    }

    //DELETE
    public async Task<bool> DeleteAsync(string ids, EDeleteType deleteType)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("Hec bir id daxil edilmiyib!");

        await EnsureActorExist(idArray);

        if (deleteType == EDeleteType.Hard)
        {
            foreach (var id in idArray)
            {
                var data = await _repo.GetByIdAsync(id, false);
                if (data != null && !string.IsNullOrEmpty(data.ImageUrl) && data.ImageUrl != defaultImage)
                    await _fileService.DeleteImageIfNotDefault(data.ImageUrl, "actors");
            }
        }

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
                await RemoveActorCacheAsync(id);
        }

        return success;
    }

    private async Task EnsureActorExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Actor>();
    }
}

