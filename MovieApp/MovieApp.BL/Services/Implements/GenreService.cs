using AutoMapper;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class GenreService : IGenreService
{
    readonly IGenreRepository _repo;
    readonly IMapper _mapper; 
    public GenreService(IGenreRepository repo, IMapper mapper)
    {
        _mapper = mapper; 
        _repo = repo; 
    }

    public async Task<int> CreateAsync(GenreCreateDto dto)
    {
        var data = _mapper.Map<Genre>(dto);
        data.CreatedTime = DateTime.Now.ToLocalTime();

        await _repo.AddAsync(data);
        await _repo.SaveAsync();
        return data.Id; 
    }

    public async Task CreateRangeAsync(params GenreCreateDto[] dtos)
    {
        var datas = _mapper.Map<IEnumerable<Genre>>(dtos);
        foreach (var data in datas)
            data.CreatedTime = DateTime.Now.ToLocalTime();

        await _repo.AddRangeAsync(datas);
        await _repo.SaveAsync(); 
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await ValidateGenreExistenceAsync(id);

        await _repo.DeleteAsync(id);
        return await _repo.SaveAsync() > 0; 
    }

    public async Task<bool> DeleteRangeAsync(params int[] ids)
    {
        await ValidateGenresExistenceAsync(ids);

        await _repo.DeleteRangeAsync(ids);
        return ids.Length == await _repo.SaveAsync();
    }

    public async Task<IEnumerable<GenreGetDto>> GetAllAsync()
    {
        var genres = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<GenreGetDto>>(genres); 
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        var genre = await _repo.GetByIdAsync(id);
        if (genre == null)
            throw new NotFoundException<Genre>();

        return _mapper.Map<GenreGetDto>(genre);
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await ValidateGenreExistenceAsync(id);

        await _repo.ReverseSoftDeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteRangeAsync(params int[] ids)
    {
        await ValidateGenresExistenceAsync(ids);

        await _repo.ReverseSoftDeleteRangeAsync(ids);
        return ids.Length == await _repo.SaveAsync();
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        await ValidateGenreExistenceAsync(id);

        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            return false;

        _repo.SoftDelete(data);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(params int[] ids)
    {
        await ValidateGenresExistenceAsync(ids);

        await _repo.SoftDeleteRangeAsync(ids);
        return ids.Length == await _repo.SaveAsync();
    }
    private async Task ValidateGenreExistenceAsync(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Genre>();
    }

    private async Task ValidateGenresExistenceAsync(int[] ids)
    {
        foreach (int id in ids)
            if (!await _repo.IsExistAsync(id))
                throw new NotFoundException<Genre>();
    }
}

