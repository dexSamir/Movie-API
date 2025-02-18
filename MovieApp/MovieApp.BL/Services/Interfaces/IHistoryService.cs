using MovieApp.BL.DTOs.HistoryDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IHistoryService
{
    Task<IEnumerable<HistoryGetDto>> GetUserWatchHistoryAsync();
    Task<HistoryGetDto> GetLastWatchedAsync();
    Task<int> AddHistoryAsync(HistoryCreateDto dto);
    Task<bool> UpdateHistoryAsync(HistoryUpdateDto dto, int historyId);
    Task<bool> RemoveFromHistoryAsync(int historyId);
    Task<bool> ClearHistoryAsync();
}

