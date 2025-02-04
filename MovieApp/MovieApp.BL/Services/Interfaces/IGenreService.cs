using MovieApp.BL.DTOs.GenreDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IGenreService
{
    Task<int> CreateAsync(GenreCreateDto dto);
    Task CreateRangeAsync(params GenreCreateDto[] dtos);
    Task<IEnumerable<GenreGetDto>> GetAllAsync();
    Task<GenreGetDto> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(params int[] ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteRangeAsync(params int[] ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteRangeAsync(params int[] ids);
}

