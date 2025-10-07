using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Persistence.Interfaces;

public interface IRoomRepository
{
    Task<List<RoomEntity>> GetAllRooms(CancellationToken cancellationToken);
    Task<RoomEntity?> GetRoomById(Guid id, CancellationToken cancellationToken);
    Task AddRoom(RoomEntity room, CancellationToken cancellationToken);
    Task UpdateRoom(RoomEntity room, CancellationToken cancellationToken);
    Task DeleteRoom(RoomEntity room, CancellationToken cancellationToken);
    
    Task<List<RoomEntity>> GetByCity(string city, CancellationToken cancellationToken);
    Task<List<RoomEntity>> GetByAvailableDates(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}