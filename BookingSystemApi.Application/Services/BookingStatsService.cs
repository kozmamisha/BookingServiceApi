using System.Data;
using BookingSystemApi.Application.Dto;
using BookingSystemApi.Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace BookingSystemApi.Application.Services;

public class BookingStatsService(IConfiguration configuration) : IBookingStatsService
{
    private readonly IDbConnection _db = new MySqlConnection(configuration.GetConnectionString("BookingSystemDbContext"));

    public async Task<IEnumerable<BookingStatsDto>> GetBookingStatsAsync(DateTime startDate, DateTime endDate)
    {
        const string sql = @"
            SELECT 
                h.Name AS HotelName,
                COUNT(b.Id) AS BookingCount
            FROM Hotels h
            LEFT JOIN Rooms r ON r.HotelId = h.Id
            LEFT JOIN Bookings b ON b.RoomId = r.Id
                AND b.StartTime >= @StartDate
                AND b.EndTime <= @EndDate
            GROUP BY h.Name
            ORDER BY BookingCount DESC";

        return await _db.QueryAsync<BookingStatsDto>(sql, new { StartDate = startDate, EndDate = endDate });
    }
}