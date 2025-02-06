using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.Image;
using MovieApp.BL.Extensions;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class DirectorService : IDirectorService
{
    readonly IMapper _mapper; 
    readonly IDirectorRepository _repo;
    public DirectorService(IDirectorRepository repo, IMapper mapper)
    {
        _mapper = mapper; 
        _repo = repo;
    }

    private const string defaultImage = "imgs/directors/default.png";

    public async Task<IEnumerable<DirectorGetDto>> GetAllAsync()
    {
        var directors = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<DirectorGetDto>>(directors);
    }

    public async Task<DirectorGetDto> GetByIdAsync(int id)
    {
        var director = await _repo.GetByIdAsync(id);
        if (director == null)
            throw new NotFoundException<Genre>();

        return _mapper.Map<DirectorGetDto>(director);
    }

    public async Task<int> CreateAsync(DirectorCreateDto dto)
    {

        var director = _mapper.Map<Director>(dto);
        director.CreatedTime = DateTime.UtcNow;
        director.ImageUrl = dto.ImageUrl == null || dto.ImageUrl.Length == 0
            ? defaultImage
            : await ProcessImageAsync(dto.ImageUrl);


        await _repo.AddAsync(director);
        await _repo.SaveAsync();
        return director.Id; 
    }

    public async Task CreateRangeAsync(IEnumerable<DirectorCreateDto> dtos)
    {
        var datas = _mapper.Map<IEnumerable<Director>>(dtos);
        foreach (var data in datas)
            data.CreatedTime = DateTime.UtcNow;

        foreach (var (dto, director) in dtos.Zip(datas))
        {
            director.CreatedTime = DateTime.UtcNow;
            director.ImageUrl = dto.ImageUrl == null || dto.ImageUrl.Length == 0
                ? defaultImage
                : await ProcessImageAsync(dto.ImageUrl);
        }
        await _repo.AddRangeAsync(datas);
        await _repo.SaveAsync();
    }

    public async Task<bool> UpdateAsync(DirectorUpdateDto dto, int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Director>();

        _mapper.Map(dto, data);
        if (dto.ImageUrl != null)
        {
            await DeleteImageIfNotDefault(data.ImageUrl);
            data.ImageUrl = await ProcessImageAsync(dto.ImageUrl);
        }

        data.UpdatedTime = DateTime.UtcNow;
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Director>();

        if (!string.IsNullOrEmpty(data.ImageUrl) && data.ImageUrl != defaultImage)
        {
            string filePath = Path.Combine("wwwroot", "imgs", "directors", data.ImageUrl);
            FileExtension.DeleteFile(filePath);
        }
        await _repo.DeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureDirectorExist(idArray);

        foreach (var id in idArray)
        {
            var data = await _repo.GetByIdAsync(id, false);
            if (data != null)
                await DeleteImageIfNotDefault(data.ImageUrl);
        }

        await _repo.DeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await EnsureDirectorExists(id);

        await _repo.ReverseSoftDeleteAsync(id);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        await EnsureDirectorExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            throw new NotFoundException<Director>();

        _repo.SoftDelete(data);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(string ids)
    {
        var idArray = ParseIds(ids);
        await EnsureDirectorExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }




    private static int[] ParseIds(string ids) =>
        ids.Split(',').Select(int.Parse).ToArray();

    private async Task EnsureDirectorExists(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Director>();
    }

    private async Task EnsureDirectorExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Director>();
    }

    private async Task<string> ProcessImageAsync(IFormFile image)
    {
        if (!image.IsValidType("image"))
            throw new UnsupportedFileTypeException();

        if (!image.IsValidSize(5))
            throw new UnsupportedFileSizeException(5);

        return await image.UploadAsync("wwwroot", "imgs", "directors");
    }

    private async Task DeleteImageIfNotDefault(string imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl) && imageUrl != defaultImage)
        {
            string filePath = Path.Combine("wwwroot", "imgs", "directors", imageUrl);
            FileExtension.DeleteFile(filePath);
        }
    }
}

