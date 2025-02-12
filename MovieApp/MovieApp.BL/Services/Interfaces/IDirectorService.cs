using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.Services.Interfaces;
public interface IDirectorService
{
    Task<int> CreateAsync(DirectorCreateDto dto);
    Task<bool> UpdateAsync(DirectorUpdateDto dto, int id);
    Task<IEnumerable<DirectorGetDto>> GetAllAsync();
    Task<DirectorGetDto> GetByIdAsync(int id);

    Task<bool> DeleteAsync(string ids, EDeleteType deleteType);
}

