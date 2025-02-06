using System;
using MovieApp.BL.DTOs.DirectorDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IDirectorService
{
    Task<int> CreateAsync(DirectorCreateDto dto);
    Task CreateRangeAsync(IEnumerable<DirectorCreateDto> dtos);
    Task<bool> UpdateAsync(DirectorUpdateDto dto, int id);
    Task<IEnumerable<DirectorGetDto>> GetAllAsync();
    Task<DirectorGetDto> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(string ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteRangeAsync(string ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteRangeAsync(string ids);
}

