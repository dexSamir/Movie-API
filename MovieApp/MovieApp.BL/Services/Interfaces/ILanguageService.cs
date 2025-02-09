using MovieApp.BL.DTOs.LanguageDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface ILanguageService
{
    Task<string> CreateAsync(LanguageCreateDto dto);
    Task<bool> UpdateAsync(LanguageUpdateDto dto, int id);
    Task<IEnumerable<LanguageGetDto>> GetAllAsync();

    Task<LanguageGetDto> GetByIdAsync(int id);
    Task<LanguageGetDto> GetByCodeAsync(string code);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteAsync(string code);
    Task<bool> DeleteRangeAsync(string ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteAsync(string code);
    Task<bool> SoftDeleteRangeAsync(string ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteAsync(string code);
    Task<bool> ReverseDeleteRangeAsync(string ids);
}

