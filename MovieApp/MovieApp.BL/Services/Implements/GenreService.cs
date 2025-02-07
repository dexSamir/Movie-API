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
        data.CreatedTime = DateTime.UtcNow;

        await _repo.AddAsync(data);
        await _repo.SaveAsync();
        return data.Id; 
    }

    public async Task CreateRangeAsync(IEnumerable<GenreCreateDto> dtos)
    {
        var datas = _mapper.Map<IEnumerable<Genre>>(dtos);
        foreach (var data in datas)
            data.CreatedTime = DateTime.UtcNow;

        await _repo.AddRangeAsync(datas);
        await _repo.SaveAsync(); 
    }

    public async Task<bool> UpdateAsync(int id, GenreUpdateDto dto)
    {
        await EnsureGenreExists(id);
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Genre>();

        _mapper.Map(dto, data);
        data.UpdatedTime = DateTime.UtcNow;
        return await _repo.SaveAsync() > 0; 
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await EnsureGenreExists(id);

        await _repo.DeleteAsync(id);
        return await _repo.SaveAsync() > 0; 
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        await EnsureGenresExist(idArray);

        await _repo.DeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<IEnumerable<GenreGetDto>> GetAllAsync()
    {
        var genres = await _repo.GetAllAsync();
        var datas = _mapper.Map<IEnumerable<GenreGetDto>>(genres);

        foreach (var dto in datas)
        {
            var genre = genres.FirstOrDefault(x => x.Id == dto.Id);
            if(genre != null)
            {
                dto.MovieCount = genre.Movies?.Count ?? 0; 
                dto.SerieCount = genre.Series?.Count ?? 0;
            }
        }

        return datas;
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        var genre = await _repo.GetByIdAsync(id);
        if (genre == null)
            throw new NotFoundException<Genre>();

        var data = _mapper.Map<GenreGetDto>(genre);
        data.SerieCount = genre.Series?.Count ?? 0;
        data.MovieCount = genre.Movies?.Count ?? 0;
        return data;
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await EnsureGenreExists(id);

        await _repo.ReverseSoftDeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        await EnsureGenresExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        await EnsureGenreExists(id);

        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            return false;

        _repo.SoftDelete(data);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        await EnsureGenresExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    private static int[] ParseIds(string ids) =>
        ids.Split(',').Select(int.Parse).ToArray();

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

