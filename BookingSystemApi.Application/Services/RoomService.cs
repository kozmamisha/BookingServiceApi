using BookingSystemApi.Application.Exceptions;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;

namespace BookingSystemApi.Application.Services;

public class RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository) : IRoomService
{
    public async Task<List<RoomEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await roomRepository.GetAllRooms(cancellationToken);
    }

    public async Task<RoomEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await roomRepository.GetRoomById(id, cancellationToken)
               ?? throw new EntityNotFoundException("Room not found");
    }

    public async Task CreateAsync(decimal pricePerNight, int capacity, Guid hotelId, CancellationToken cancellationToken)
    {
        if (pricePerNight <= 0 || capacity <= 0)
            throw new ArgumentException("Value must be greater than zero.");
        
        var hotel = await hotelRepository.GetHotelById(hotelId, cancellationToken)
                   ?? throw new EntityNotFoundException("Hotel not found");
        
        RoomEntity room = new()
        {
            PricePerNight = pricePerNight,
            Capacity = capacity,
            HotelId = hotelId,
        };
        
        await roomRepository.AddRoom(room, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, decimal pricePerNight, int capacity, CancellationToken cancellationToken)
    {
        if (pricePerNight <= 0 || capacity <= 0)
            throw new ArgumentException("Value must be greater than zero.");
        
        var currentRoom = await roomRepository.GetRoomById(id, cancellationToken)
                             ?? throw new EntityNotFoundException("Room not found");
        
        currentRoom.PricePerNight = pricePerNight;
        currentRoom.Capacity = capacity;
        
        await roomRepository.UpdateRoom(currentRoom, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetRoomById(id, cancellationToken)
                      ?? throw new EntityNotFoundException("Room not found");

        await roomRepository.DeleteRoom(room, cancellationToken);
    }

    public async Task<List<RoomEntity>> GetByAddressAsync(string address, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Incorrect address.");
        
        return await roomRepository.GetByAddress(address, cancellationToken);
    }

    public async Task<List<RoomEntity>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        if (startDate == default || endDate == default)
            throw new ArgumentException("Start or end date cannot be empty.");

        if (startDate >= endDate)
            throw new ArgumentException("Start date must be earlier than end date.");

        return await roomRepository.GetByAvailableDates(startDate, endDate, cancellationToken);
    }
}