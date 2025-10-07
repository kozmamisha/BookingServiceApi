using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Application.Interfaces;

public interface IRoomService
{
    Task<List<RoomEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<RoomEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(decimal pricePerNight, int capacity, Guid hotelId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, decimal pricePerNight, int capacity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<RoomEntity>> GetByCityAsync(string city, CancellationToken cancellationToken);
    Task<List<RoomEntity>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}