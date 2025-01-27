using System;
using MovieApp.BL.DTOs.DirectorDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IDirectorService
{
    Task<int> CreateAsync(DirectorCreateDto dto);
    Task CreateRangeAsync(params DirectorCreateDto[] dtos);
    Task UpdateAsync(DirectorUpdateDto dto, int? id);
    Task<bool> DeleteAsync(int? id);
    Task<IEnumerable<DirectoryGetDto>> GetAllAsync();
    Task<DirectoryGetDto> GetById(int? id);
}

