using BookingSystemApi.Application.Dto;

namespace BookingSystemApi.Application.Interfaces;

public interface IBookingStatsService
{
    Task<IEnumerable<BookingStatsDto>> GetBookingStatsAsync(DateTime startDate, DateTime endDate);
}