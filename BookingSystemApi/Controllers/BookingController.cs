using System.Security.Claims;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Contracts.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingUpsertRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        await bookingService.CreateAsync(request.StartTime, request.EndTime, request.RoomId, userId, cancellationToken);
        return Created();
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken)
    {
        var bookings = await bookingService.GetAllAsync(cancellationToken);
        return Ok(bookings);
    }
    
    [HttpGet("user")]
    public async Task<IActionResult> GetAllBookingsByUserId(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        
        var bookings = await bookingService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(bookings);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpsertRequest request, CancellationToken cancellationToken)
    {
        await bookingService.UpdateAsync(id, request.StartTime, request.EndTime, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBooking(Guid id, CancellationToken cancellationToken)
    {
        await bookingService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}