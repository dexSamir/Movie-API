using MovieApp.BL.DTOs.ActorDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IActorService
{

    Task<int> CreateAsync(ActorCreateDto dto);
    Task<bool> UpdateAsync(ActorUpdateDto dto, int id);
    Task<IEnumerable<ActorGetDto>> GetAllAsync();
    Task<ActorGetDto> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(string ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteRangeAsync(string ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteRangeAsync(string ids);
}

