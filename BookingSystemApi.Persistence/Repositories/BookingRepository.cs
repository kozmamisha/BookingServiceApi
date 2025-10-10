using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystemApi.Persistence.Repositories;

public class BookingRepository(BookingSystemDbContext dbContext) : IBookingRepository
{
    public async Task<List<BookingEntity>> GetAllBookings(CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .Include(b => b.User)
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BookingEntity>> GetAllBookingsByUserId(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .Include(b => b.User)
            .Where(b => b.UserId == userId)
            .ToListAsync(cancellationToken);
    }   

    public async Task<BookingEntity?> GetBookingById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .Include(b => b.User)
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddBooking(BookingEntity booking, CancellationToken cancellationToken)
    {
        booking.StartTime = DateTime.UtcNow;
        
        await dbContext.Bookings.AddAsync(booking, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateBooking(BookingEntity booking, CancellationToken cancellationToken)
    {
        dbContext.Bookings.Update(booking);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBooking(BookingEntity booking, CancellationToken cancellationToken)
    {
        dbContext.Bookings.Remove(booking);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<BookingEntity>> GetOverlappingBookingsAsync(Guid roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Where(b => b.RoomId == roomId &&
                        ((startDate >= b.StartTime && startDate < b.EndTime) ||
                         (endDate > b.StartTime && endDate <= b.EndTime) ||
                         (startDate <= b.StartTime && endDate >= b.EndTime)))
            .ToListAsync(cancellationToken);
    }
}