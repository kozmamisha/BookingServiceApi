using System.Security.Claims;
using AutoMapper;
using BookingSystemApi.Application.Dto;
using BookingSystemApi.Application.Exceptions;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookingSystemApi.Application.Services;

public class BookingService(
    IBookingRepository bookingRepository, 
    IRoomRepository roomRepository,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    UserManager<UserEntity> userManager) : IBookingService
{
    private string CurrentUserId =>
        httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    
    public async Task<List<BookingDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var bookings = await bookingRepository.GetAllBookings(cancellationToken);
        return mapper.Map<List<BookingDto>>(bookings);
    }

    public async Task<List<BookingDto>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var bookings = await bookingRepository.GetAllBookingsByUserId(userId, cancellationToken);
        return mapper.Map<List<BookingDto>>(bookings);
    }

    public async Task CreateAsync(DateTime startTime, DateTime endTime, Guid roomId, string userId, CancellationToken cancellationToken)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Invalid booking dates");

        var room = await roomRepository.GetRoomById(roomId, cancellationToken)
            ?? throw new EntityNotFoundException("Room not found");
        
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new EntityNotFoundException("User not found");

        var overlappingBookings = await bookingRepository.GetOverlappingBookingsAsync(roomId, startTime, endTime, cancellationToken);
        if (overlappingBookings.Any())
            throw new ArgumentException("Room is not available for the selected dates");

        var booking = new BookingEntity
        {
            RoomId = roomId,
            UserId = userId,
            StartTime = startTime,
            EndTime = endTime
        };

        await bookingRepository.AddBooking(booking, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingById(id, cancellationToken)
            ?? throw new EntityNotFoundException("Booking not found");
        
        if (CurrentUserId != booking.UserId)
            throw new ArgumentException("You can not update other user's booking");
        
        if (startTime >= endTime)
            throw new ArgumentException("Invalid booking dates");
        
        var overlappingBookings = await bookingRepository.GetOverlappingBookingsAsync(booking.RoomId, startTime, endTime, cancellationToken);
        if (overlappingBookings.Any(b => b.Id != id))
            throw new ArgumentException("Room is not available for the selected dates");
        
        booking.StartTime = startTime;
        booking.EndTime = endTime;
        
        await bookingRepository.UpdateBooking(booking, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingById(id, cancellationToken)
                      ?? throw new EntityNotFoundException("Booking not found");
        
        if (CurrentUserId != booking.UserId)
            throw new ArgumentException("You can not delete other user's booking");
        
        await bookingRepository.DeleteBooking(booking, cancellationToken);
    }
}