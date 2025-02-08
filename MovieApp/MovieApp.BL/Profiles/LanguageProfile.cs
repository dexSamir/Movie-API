using AutoMapper;
using MovieApp.BL.DTOs.LanguageDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class LanguageProfile : Profile
{
	public LanguageProfile()
	{
        CreateMap<LanguageCreateDto, Language>();
        CreateMap<LanguageUpdateDto, Language>();
        CreateMap<Language, LanguageGetDto>();
    }
}

