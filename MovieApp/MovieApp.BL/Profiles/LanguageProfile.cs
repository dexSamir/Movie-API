using AutoMapper;
using MovieApp.BL.DTOs.LanguageDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class LanguageProfile : Profile
{
	public LanguageProfile()
	{
        CreateMap<LanguageCreateDto, Language>();
        CreateMap<LanguageUpdateDto, Language>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && (srcMember is not string || !string.IsNullOrWhiteSpace(srcMember.ToString()))
            ));
        CreateMap<Language, LanguageGetDto>();
            
    }
}

