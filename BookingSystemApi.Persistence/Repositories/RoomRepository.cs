using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystemApi.Persistence.Repositories;

public class RoomRepository(BookingSystemDbContext dbContext) : IRoomRepository
{
    public async Task<List<RoomEntity>> GetAllRooms(CancellationToken cancellationToken)
    {
        return await dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .ToListAsync(cancellationToken);
    }

    public async Task<RoomEntity?> GetRoomById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task AddRoom(RoomEntity room, CancellationToken cancellationToken)
    {
        await dbContext.Rooms.AddAsync(room, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRoom(RoomEntity room, CancellationToken cancellationToken)
    {
        dbContext.Rooms.Update(room);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRoom(RoomEntity room, CancellationToken cancellationToken)
    {
        dbContext.Rooms.Remove(room);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<RoomEntity>> GetByAddress(string address, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .Where(r => r.Hotel != null && r.Hotel.Address.Contains(address))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<RoomEntity>> GetByAvailableDates(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms
            .AsNoTracking()
            .Include(r => r.Bookings)
            .Include(r => r.Hotel)
            .Where(r => !r.Bookings.Any(b =>
                startDate < b.StartTime && endDate > b.EndTime))
            .ToListAsync(cancellationToken);
    }
}