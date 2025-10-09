using BookingSystemApi.Application.Exceptions;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BookingSystemApi.Application.Services;

public class BookingService(
    IBookingRepository bookingRepository, 
    IRoomRepository roomRepository,
    IHttpContextAccessor httpContextAccessor) : IBookingService
{
    // private Guid CurrentUserId => Guid.Parse(
    //     httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaims.UserId)!);
    
    public async Task<List<BookingEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await bookingRepository.GetAllBookings(cancellationToken);
    }

    public async Task<BookingEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await bookingRepository.GetBookingById(id, cancellationToken)
            ?? throw new EntityNotFoundException("Booking not found");
    }

    public async Task CreateAsync(DateTime startTime, DateTime endTime, Guid roomId, CancellationToken cancellationToken)
    {
        // if (startTime == default || endTime == default)
        //     throw new ArgumentException("Start or end date cannot be empty.");
        //
        // var room = await roomRepository.GetRoomById(roomId, cancellationToken)
        //            ?? throw new EntityNotFoundException("Room not found");
        //
        // BookingEntity booking = new()
        // {
        //     StartTime = startTime,
        //     EndTime = endTime,
        //     RoomId = roomId,
        //     UserId = Guid.NewGuid() // add current user
        // };
        //
        // await bookingRepository.AddBooking(booking, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        // if (startTime == default || endTime == default)
        //     throw new ArgumentException("Start or end date cannot be empty.");
        //
        // var currentBooking = await bookingRepository.GetBookingById(id, cancellationToken)
        //            ?? throw new EntityNotFoundException("Booking not found");
        //
        // // if (CurrentUserId != currentComment.AuthorId)
        // // {
        // //     throw new BadRequestException("You can not update other person's comment");
        // // }
        //
        // currentBooking.StartTime = startTime;
        // currentBooking.EndTime = endTime;
        //
        // await bookingRepository.UpdateBooking(currentBooking, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        // var booking = await bookingRepository.GetBookingById(id, cancellationToken)
        //               ?? throw new EntityNotFoundException("Booking not found");
        //     
        // // if (CurrentUserId != comment.AuthorId)
        // // {
        // //     throw new BadRequestException("You can not delete other person's comment");
        // // }
        //
        // await bookingRepository.DeleteBooking(booking, cancellationToken);
    }
}