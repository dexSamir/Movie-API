using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.Services.Interfaces;
public interface IGenreService
{
    Task<int> CreateAsync(GenreCreateDto dto);
    Task CreateRangeAsync(IEnumerable<GenreCreateDto> dtos);

    Task<IEnumerable<GenreGetDto>> GetAllAsync();
    Task<GenreGetDto> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, GenreUpdateDto dto);

    Task<bool> DeleteAsync(string ids, EDeleteType deleteType); 
}

