using AutoMapper;
using MovieApp.BL.DTOs.RentalDtos;
using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class RentalService : IRentalService
{
    private readonly IRentalRepository _repo;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public RentalService(IRentalRepository repo, ICurrentUser currentUser, IMapper mapper)
    {
        _repo = repo;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    private readonly string[] _includeProperties =
    {
        "Movie", "User"
    };

    private async Task<string> GetUserIdAsync()
    {
        var userId = _currentUser.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    public async Task<IEnumerable<RentalGetDto>> GetUserRentalsAsync()
    {
        var userId = await GetUserIdAsync();

        var rentals = await _repo.GetWhereAsync(x=> x.UserId == userId, _includeProperties);
        return _mapper.Map<IEnumerable<RentalGetDto>>(rentals);
    }

    public async Task<RentalGetDto> GetRentalByIdAsync(int rentalId)
    {
        var userId = await GetUserIdAsync();

        var rental = await _repo.GetByIdAsync(rentalId, _includeProperties);
        if (rental == null || rental.UserId != userId)
            throw new NotFoundException<Rental>();

        return _mapper.Map<RentalGetDto>(rental);
    }

    public async Task<int> RentMovieAsync(RentalCreateDto dto)
    {
        var userId = await GetUserIdAsync();

        var rental = _mapper.Map<Rental>(dto);
        rental.UserId = userId;
        rental.RentalDate = DateTime.UtcNow;

        await _repo.AddAsync(rental);
        await _repo.SaveAsync();
        return rental.Id;
    }

    public async Task<bool> UpdateRentalAsync(RentalUpdateDto dto)
    {
        var userId = await GetUserIdAsync();

        var rental = await _repo.GetByIdAsync(dto.Id, _includeProperties);
        if (rental == null || rental.UserId != userId)
            throw new NotFoundException<Rental>();
        
        var data = _mapper.Map(dto, rental);
        data.UpdatedTime = DateTime.UtcNow; 
        _repo.UpdateAsync(rental);
        return await _repo.SaveAsync() > 0 ;
    }

    public async Task<bool> ReturnMovieAsync(int rentalId)
    {
        var userId = await GetUserIdAsync();

        var rental = await _repo.GetByIdAsync(rentalId, _includeProperties);
        if (rental == null || rental.UserId != userId)
            throw new NotFoundException<Rental>();

        rental.ReturnDate = DateTime.UtcNow;
        _repo.UpdateAsync(rental);
        return await _repo.SaveAsync() > 0;
    }
}

