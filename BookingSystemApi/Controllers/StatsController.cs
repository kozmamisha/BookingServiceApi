using BookingSystemApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/stats")]
[Authorize(Roles = "Admin")]
public class StatsController(IBookingStatsService statsService) : ControllerBase
{
    [HttpGet("bookings")]
    public async Task<IActionResult> GetBookingStats([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var stats = await statsService.GetBookingStatsAsync(startDate, endDate);
        return Ok(stats);
    }
}