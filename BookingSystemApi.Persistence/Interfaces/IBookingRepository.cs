using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Persistence.Interfaces;

public interface IBookingRepository
{
    Task<List<BookingEntity>> GetAllBookings(CancellationToken cancellationToken);
    Task<List<BookingEntity>> GetAllBookingsByUserId(string userId, CancellationToken cancellationToken);
    Task<BookingEntity?> GetBookingById(Guid id, CancellationToken cancellationToken);
    Task AddBooking(BookingEntity booking, CancellationToken cancellationToken);
    Task UpdateBooking(BookingEntity booking, CancellationToken cancellationToken);
    Task DeleteBooking(BookingEntity booking, CancellationToken cancellationToken);

    Task<List<BookingEntity>> GetOverlappingBookingsAsync(Guid roomId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken);
}