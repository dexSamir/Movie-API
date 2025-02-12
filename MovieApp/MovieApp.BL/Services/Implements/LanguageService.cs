using AutoMapper;
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
    private readonly ICacheService _cache;

    public LanguageService(ILanguageRepository repo, IMapper mapper, IFileService fileService, ICacheService cache)
    {
        _fileService = fileService;
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }

    private const string defaultImage = "imgs/languages/default.png";

    private const string CacheKeyPrefix = "language_";

    public async Task<IEnumerable<LanguageGetDto>> GetAllAsync()
    {
        return await _cache.GetOrSetAsync("all_languages", async () =>
        {
            var languages = await _repo.GetAllAsync("Subtitles", "AudioTracks");
            return _mapper.Map<IEnumerable<LanguageGetDto>>(languages);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<LanguageGetDto> GetByCodeAsync(string code)
    {
        var cacheKey = $"{CacheKeyPrefix}{code}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var language = await GetLanguageByCodeAsync(code);
            return _mapper.Map<LanguageGetDto>(language);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<LanguageGetDto> GetByIdAsync(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var language = await GetLanguageByIdAsync(id);
            return _mapper.Map<LanguageGetDto>(language);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<string> CreateAsync(LanguageCreateDto dto)
    {
        if (await _repo.IsExistAsync(x => x.Code == dto.Code))
            throw new ExistException<Language>();

        var language = _mapper.Map<Language>(dto);
        language.CreatedTime = DateTime.UtcNow;
        language.Icon = await _fileService.ProcessImageAsync(dto.Icon, "languages", "image/", 5);

        await _repo.AddAsync(language);
        await _repo.SaveAsync();

        await _cache.RemoveAsync("all_languages");

        return language.Code;
    }

    public async Task<bool> UpdateAsync(LanguageUpdateDto dto, int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        var result = await UpdateLanguageAsync(dto, language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        var result = await DeleteLanguageAsync(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

        await _cache.RemoveAsync("all_languages");

        return result;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        var result = await DeleteLanguageAsync(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{code}");

        await _cache.RemoveAsync("all_languages");

        return result;
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureLanguagesExist(idArray);

        var languages = await _repo.GetByIdsAsync(idArray);
        foreach (var language in languages)
            await _fileService.DeleteImageIfNotDefault(language.Icon, "languages");

        await _repo.DeleteRangeAsync(idArray);
        var success = idArray.Length == await _repo.SaveAsync();

        if (success)
        {
            foreach (var id in idArray)
                await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

            await _cache.RemoveAsync("all_languages");
        }

        return success;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        _repo.SoftDelete(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");
        await _cache.RemoveAsync("all_languages");

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteAsync(string code)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        _repo.SoftDelete(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{code}");
        await _cache.RemoveAsync("all_languages");

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        var language = await GetLanguageByIdAsync(id, false);
        _repo.ReverseSoftDelete(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");
        await _cache.RemoveAsync("all_languages");

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ReverseDeleteAsync(string code)
    {
        var language = await GetLanguageByCodeAsync(code, false);
        _repo.ReverseSoftDelete(language);

        await _cache.RemoveAsync($"{CacheKeyPrefix}{code}");
        await _cache.RemoveAsync("all_languages");

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> SoftDeleteRangeAsync(string idsOrCodes)
    {
        var idArray = FileHelper.ParseIds(idsOrCodes);
        await EnsureLanguagesExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);

        foreach (var id in idArray)
            await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

        await _cache.RemoveAsync("all_languages");

        return idArray.Length == await _repo.SaveAsync();
    }

    public async Task<bool> ReverseDeleteRangeAsync(string idsOrCodes)
    {
        var idArray = FileHelper.ParseIds(idsOrCodes);
        await EnsureLanguagesExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);

        foreach (var id in idArray)
            await _cache.RemoveAsync($"{CacheKeyPrefix}{id}");

        await _cache.RemoveAsync("all_languages");

        return idArray.Length == await _repo.SaveAsync();
    }

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
            language.Icon = await _fileService.ProcessImageAsync(dto.Icon, "languages", "image/", 5);
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

    private async Task EnsureLanguagesExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Language>();
    }
}
