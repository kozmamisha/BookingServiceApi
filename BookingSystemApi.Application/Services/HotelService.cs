using BookingSystemApi.Application.Exceptions;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;

namespace BookingSystemApi.Application.Services;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public async Task<List<HotelEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await hotelRepository.GetAllHotels(cancellationToken);
    }

    public async Task<HotelEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await hotelRepository.GetHotelById(id, cancellationToken)
            ?? throw new EntityNotFoundException("Hotel not found");
    }

    public async Task CreateAsync(string name, string address, string description, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Name, address, or description cannot be empty.");
        }

        HotelEntity hotel = new()
        {
            Name = name,
            Address = address,
            Description = description,
        };

        await hotelRepository.AddHotel(hotel, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, string name, string address, string description, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Name, address, or description cannot be empty.");
        }
        
        var currentHotel = await hotelRepository.GetHotelById(id, cancellationToken) ??
                    throw new EntityNotFoundException("Hotel not found");
        
        currentHotel.Name = name;
        currentHotel.Address = address;
        currentHotel.Description = description;
        
        await hotelRepository.UpdateHotel(currentHotel, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var hotel = await hotelRepository.GetHotelById(id, cancellationToken) ??
                    throw new EntityNotFoundException("Hotel not found");
        
        await hotelRepository.DeleteHotel(hotel, cancellationToken);
    }
}