using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.Services.Interfaces;
public interface IActorService
{

    Task<int> CreateAsync(ActorCreateDto dto);
    Task<bool> UpdateAsync(ActorUpdateDto dto, int id);
    Task<IEnumerable<ActorGetDto>> GetAllAsync();
    Task<ActorGetDto> GetByIdAsync(int id);

    Task<bool> DeleteAsync(string ids, EDeleteType deleteType);
}

