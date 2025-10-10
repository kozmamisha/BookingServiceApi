using BookingSystemApi.Application.Dto;

namespace BookingSystemApi.Application.Interfaces;

public interface IBookingService
{
    Task<List<BookingDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<BookingDto>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task CreateAsync(DateTime startDate, DateTime endTime, Guid roomId, string userId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, DateTime startDate, DateTime endTime, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}