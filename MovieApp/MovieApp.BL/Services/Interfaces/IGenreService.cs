using Microsoft.VisualBasic;
using MovieApp.BL.DTOs.GenreDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IGenreService
{
    Task<int> CreateAsync(GenreCreateDto dto);
    Task CreateRangeAsync(IEnumerable<GenreCreateDto> dtos);

    Task<IEnumerable<GenreGetDto>> GetAllAsync();
    Task<GenreGetDto> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, GenreUpdateDto dto);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(string ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteRangeAsync(string ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteRangeAsync(string ids);
}

