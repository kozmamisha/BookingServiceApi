using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Application.Interfaces;

public interface IBookingService
{
    Task<List<BookingEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<BookingEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(DateTime startDate, DateTime endTime, Guid roomId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, DateTime startDate, DateTime endTime, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}