using AutoMapper;
using MovieApp.BL.DTOs.LanguageDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
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

    private const string defaultImage = "imgs/actors/default.png";

    public async Task<string> CreateAsync(LanguageCreateDto dto)
    {
        if (await _repo.IsExistAsync(x => x.Code == dto.Code))
            throw new ExistException<Language>();

        var language = _mapper.Map<Language>(dto);
        language.CreatedTime = DateTime.UtcNow;
        language.Icon = dto.Icon == null || dto.Icon.Length == 0
            ? defaultImage
            : await _fileService.ProcessImageAsync(dto.Icon, "languages");

        await _repo.AddAsync(language);
        await _repo.SaveAsync();
        return language.Code; 
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRangeAsync(string idsOrCodes)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LanguageGetDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LanguageGetDto> GetByCodeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<LanguageGetDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReverseDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReverseDeleteAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReverseDeleteRangeAsync(string idsOrCodes)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteRangeAsync(string idsOrCodes)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(LanguageUpdateDto dto, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(LanguageUpdateDto dto, string code)
    {
        throw new NotImplementedException();
    }
}

