using BookingSystemApi.Application.Dto;

namespace BookingSystemApi.Application.Interfaces;

public interface IHotelService
{
    Task<List<HotelDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<HotelDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(string name, string address, string description, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, string name, string address, string description, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}