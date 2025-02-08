using MovieApp.BL.DTOs.LanguageDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface ILanguageService
{
    Task<int> CreateAsync(LanguageCreateDto dto);
    Task<bool> UpdateAsync(LanguageUpdateDto dto, int id);
    Task<bool> UpdateAsync(LanguageUpdateDto dto, string code);
    Task<IEnumerable<LanguageGetDto>> GetAllAsync();

    Task<LanguageGetDto> GetByIdAsync(int id);
    Task<LanguageGetDto> GetByCodeAsync(int id);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteAsync(string code);

    Task<bool> DeleteRangeAsync(string idsOrCodes);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteAsync(string code);
    Task<bool> SoftDeleteRangeAsync(string idsOrCodes);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteAsync(string code);
    Task<bool> ReverseDeleteRangeAsync(string idsOrCodes);
}

