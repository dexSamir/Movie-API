using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieApp.BL.DTOs.LanguageDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class LanguageService : ILanguageService
{
    readonly ILanguageRepository _repo;
    readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public LanguageService(ILanguageRepository repo, IMapper mapper, IFileService fileService)
    {
        _fileService = fileService;
        _repo = repo;
        _mapper = mapper;
    }

    private const string defaultImage = "imgs/languages/default.png";

    public async Task<IEnumerable<LanguageGetDto>> GetAllAsync()
    {
        var languages = await _repo.GetAllAsync("Subtitles", "AudioTracks");
        return _mapper.Map<IEnumerable<LanguageGetDto>>(languages);
    }

    public async Task<LanguageGetDto> GetByCodeAsync(string code)
    {
        var language = await GetLanguageByCodeAsync(code);
        return _mapper.Map<LanguageGetDto>(language);
    }

    public async Task<LanguageGetDto> GetByIdAsync(int id)
    {
        var language = await GetLanguageByIdAsync(id);
        return _mapper.Map<LanguageGetDto>(language);
    }

    public async Task<string> CreateAsync(LanguageCreateDto dto)
    {
        if (await _repo.IsExistAsync(x => x.Code == dto.Code))
            throw new ExistException<Language>();

        var language = _mapper.Map<Language>(dto);
        language.CreatedTime = DateTime.UtcNow;
        language.Icon = await ProcessImageAsync(dto.Icon);

        await _repo.AddAsync(language);
        await _repo.SaveAsync();
        return language.Code; 
    }

    public async Task<bool> UpdateAsync(LanguageUpdateDto dto, int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        return await UpdateLanguageAsync(dto, language);
    }

    public async Task<bool> UpdateAsync(LanguageUpdateDto dto, string code)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        return await UpdateLanguageAsync(dto, language);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        return await DeleteLanguageAsync(language);
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        return await DeleteLanguageAsync(language);
    }

    public async Task<bool> DeleteRangeAsync(string idsOrCodes)
    {
        var idArray = FileHelper.ParseIds(idsOrCodes);
        await EnsureLanguagesExist(idArray);

        var languages = await _repo.GetByIdsAsync(idArray);
        foreach (var language in languages)
            await _fileService.DeleteImageIfNotDefault(language.Icon, "languages");

        await _repo.DeleteRangeAsync(idArray);
        return idArray.Length == await _repo.SaveAsync();
    }


    public async Task<bool> ReverseDeleteAsync(int id) => await ToggleSoftDeleteAsync(id, true);
    public async Task<bool> ReverseDeleteAsync(string code) => await ToggleSoftDeleteAsync(code, true);
    public async Task<bool> ReverseDeleteRangeAsync(string idsOrCodes) => await ToggleSoftDeleteRangeAsync(idsOrCodes, true);

    public async Task<bool> SoftDeleteAsync(int id) => await ToggleSoftDeleteAsync(id, false);
    public async Task<bool> SoftDeleteAsync(string code) => await ToggleSoftDeleteAsync(code, false);
    public async Task<bool> SoftDeleteRangeAsync(string idsOrCodes) => await ToggleSoftDeleteRangeAsync(idsOrCodes, false);



    private async Task<Language> GetLanguageByIdAsync(int id, bool trackChanges = true)
    {
        var language = await _repo.GetByIdAsync(id, trackChanges);
        return language ?? throw new NotFoundException<Language>();
    }

    private async Task<Language> GetLanguageByCodeAsync(string code, bool trackChanges = true)
    {
        var language = await _repo.GetFirstAsync(x => x.Code == code, trackChanges);
        return language ?? throw new NotFoundException<Language>();
    }

    private async Task<bool> UpdateLanguageAsync(LanguageUpdateDto dto, Language language)
    {
        _mapper.Map(dto, language);

        if (dto.Icon != null)
        {
            await _fileService.DeleteImageIfNotDefault(language.Icon, "languages");
            language.Icon = await _fileService.ProcessImageAsync(dto.Icon, "languages");
        }

        language.UpdatedTime = DateTime.UtcNow;
        return await _repo.SaveAsync() > 0;
    }

    private async Task<bool> DeleteLanguageAsync(Language language)
    {
        await _fileService.DeleteImageIfNotDefault(language.Icon, "languages");
        _repo.Delete(language);
        return await _repo.SaveAsync() > 0;
    }

    private async Task<bool> ToggleSoftDeleteAsync(int id, bool reverse)
    {
        var language = await GetLanguageByIdAsync(id, false);
        if (reverse) _repo.ReverseSoftDelete(language);
        else _repo.SoftDelete(language);
        return await _repo.SaveAsync() > 0;
    }

    private async Task<bool> ToggleSoftDeleteAsync(string code, bool reverse)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        if (reverse) _repo.ReverseSoftDelete(language);
        else _repo.SoftDelete(language);
        return await _repo.SaveAsync() > 0;
    }

    private async Task<bool> ToggleSoftDeleteRangeAsync(string idsOrCodes, bool reverse)
    {
        var idArray = FileHelper.ParseIds(idsOrCodes);
        await EnsureLanguagesExist(idArray);

        if (reverse) await _repo.ReverseSoftDeleteRangeAsync(idArray);
        else await _repo.SoftDeleteRangeAsync(idArray);

        return idArray.Length == await _repo.SaveAsync();
    }

    private async Task EnsureLanguagesExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Language>();
    }

    private async Task<string> ProcessImageAsync(IFormFile icon)
    {
        return (icon == null || icon.Length == 0)
            ? defaultImage
            : await _fileService.ProcessImageAsync(icon, "languages");
    }
}

