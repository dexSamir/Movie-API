using System;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class DirectorService : IDirectorService
{
    readonly IDirectorRepository _repo;
    public DirectorService(IDirectorRepository repo)
    {
        _repo = repo;
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

