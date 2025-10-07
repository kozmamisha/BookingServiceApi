using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Persistence.Interfaces;

public interface IHotelRepository
{
    Task<List<HotelEntity>> GetAllHotels(CancellationToken cancellationToken);
    Task<HotelEntity?> GetHotelById(Guid id, CancellationToken cancellationToken);
    Task AddHotel(HotelEntity hotel, CancellationToken cancellationToken);
    Task UpdateHotel(HotelEntity hotel,CancellationToken cancellationToken);
    Task DeleteHotel(HotelEntity hotel, CancellationToken cancellationToken);
}