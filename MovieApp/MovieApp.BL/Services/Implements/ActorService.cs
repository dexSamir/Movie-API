using AutoMapper;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements; 
public class ActorService : IActorService
{

    readonly IMapper _mapper;
    readonly IActorRepository _repo;
    private readonly IFileService _fileService;
    public ActorService(IActorRepository repo, IMapper mapper, IFileService fileService)
    {
        _fileService = fileService;
        _mapper = mapper;
        _repo = repo;
    }

    private const string defaultImage = "imgs/actors/default.png";

    public async Task<IEnumerable<ActorGetDto>> GetAllAsync()
    {
        var actors = await _repo.GetAllAsync("Movies", "Series");
        var datas = _mapper.Map<IEnumerable<ActorGetDto>>(actors);

        foreach (var dto in datas)
        {
            var actor = actors.FirstOrDefault(d => d.Id == dto.Id);
            if (actor != null)
            {
                dto.MoviesCount = actor.Movies?.Count ?? 0;
                dto.MoviesCount = actor.Series?.Count ?? 0;
            }
        }
        return datas;
    }

    public async Task<ActorGetDto> GetByIdAsync(int id)
    {
        var actor = await _repo.GetByIdAsync(id, "Movies", "Series");
        if (actor == null)
            throw new NotFoundException<Actor>();

        var data = _mapper.Map<ActorGetDto>(actor);
        data.SeriesCount = actor.Series?.Count ?? 0;
        data.MoviesCount = actor.Movies?.Count ?? 0;
        return data;
    }

    public async Task<int> CreateAsync(ActorCreateDto dto)
    {
        var actor = _mapper.Map<Actor>(dto);
        actor.CreatedTime = DateTime.UtcNow;
        actor.ImageUrl = dto.ImageUrl == null || dto.ImageUrl.Length == 0
            ? defaultImage
            : await _fileService.ProcessImageAsync(dto.ImageUrl, "actors");


        await _repo.AddAsync(actor);
        await _repo.SaveAsync();
        return actor.Id;
    }

    public async Task<bool> UpdateAsync(ActorUpdateDto dto, int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Actor>();

        _mapper.Map(dto, data);
        if (dto.ImageUrl != null)
        {
            await _fileService.DeleteImageIfNotDefault(data.ImageUrl, "actors");
            data.ImageUrl = await _fileService.ProcessImageAsync(dto.ImageUrl, "actors");
        }

        data.UpdatedTime = DateTime.UtcNow;
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Actor>();

        if (!string.IsNullOrEmpty(data.ImageUrl) && data.ImageUrl != defaultImage)
        {
            string filePath = Path.Combine("wwwroot", "imgs", "actors", data.ImageUrl);
            FileExtension.DeleteFile(filePath);
        }
        await _repo.DeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureActorExist(idArray);

        foreach (var id in idArray)
        {
            var data = await _repo.GetByIdAsync(id, false);
            if (data != null)
                await _fileService.DeleteImageIfNotDefault(data.ImageUrl, "actors");
        }

        await _repo.DeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await EnsureActorExists(id);

        await _repo.ReverseSoftDeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureActorExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            throw new NotFoundException<Actor>();

        _repo.SoftDelete(data);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureActorExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }


    private async Task EnsureActorExists(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Actor>();
    }

    private async Task EnsureActorExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Actor>();
    }
}

