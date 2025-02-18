using MovieApp.BL.DTOs.RentalDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IRentalService
{
    Task<IEnumerable<RentalGetDto>> GetUserRentalsAsync();
    Task<RentalGetDto> GetRentalByIdAsync(int rentalId);
    Task<int> RentMovieAsync(RentalCreateDto dto);
    Task<bool> UpdateRentalAsync(RentalUpdateDto dto, int rentalId);
    Task<bool> ReturnMovieAsync(int rentalId);
}

