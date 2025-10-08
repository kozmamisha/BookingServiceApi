using BookingSystemApi.Application.Dto;
using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Application.Interfaces;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<RoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(decimal pricePerNight, int capacity, Guid hotelId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, decimal pricePerNight, int capacity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<RoomDto>> GetByAddressAsync(string address, CancellationToken cancellationToken);
    Task<List<RoomDto>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}