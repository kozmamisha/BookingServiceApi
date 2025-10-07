using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystemApi.Persistence.Repositories;

public class HotelRepository(BookingSystemDbContext dbContext) : IHotelRepository
{
    public async Task<List<HotelEntity>> GetAllHotels(CancellationToken cancellationToken)
    {
        return await dbContext.Hotels
            .AsNoTracking()
            .Include(h => h.Rooms)
            .ToListAsync(cancellationToken);
    }

    public async Task<HotelEntity?> GetHotelById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Hotels
            .AsNoTracking()
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task AddHotel(HotelEntity hotel, CancellationToken cancellationToken)
    {
        await dbContext.Hotels.AddAsync(hotel, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateHotel(HotelEntity hotel, CancellationToken cancellationToken)
    {
        dbContext.Hotels.Update(hotel);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteHotel(HotelEntity hotel, CancellationToken cancellationToken)
    {
        dbContext.Hotels.Remove(hotel);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}