using System;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.BL.Services.Interfaces;

namespace MovieApp.BL.Services.Implements;
public class DirectorService : IDirectorService
{
    readonly IDirectorService _service;
    public DirectorService(IDirectorService service)
    {
        _service = service;
    }

    public Task<int> CreateAsync(DirectorCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task CreateRangeAsync(params DirectorCreateDto[] dtos)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DirectoryGetDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DirectoryGetDto> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DirectorUpdateDto dto, int? id)
    {
        throw new NotImplementedException();
    }
}

