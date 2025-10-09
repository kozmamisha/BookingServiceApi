using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Contracts.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController(IRoomService roomService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateRoom([FromBody] RoomCreateRequest request, [FromRoute] CancellationToken cancellationToken)
    {
        await roomService.CreateAsync(request.PricePerNight, request.Capacity, request.HotelId, cancellationToken);
        return Created();
    }
    
    [HttpGet()]
    [Authorize]
    public async Task<ActionResult> GetAllRooms(CancellationToken cancellationToken)
    {
        var rooms = await roomService.GetAllAsync(cancellationToken);
        return Ok(rooms);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOneRoom([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetByIdAsync(id, cancellationToken);
        return Ok(room);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateRoom([FromRoute] Guid id, [FromBody] RoomUpdateRequest request, CancellationToken cancellationToken)
    {
        await roomService.UpdateAsync(id, request.PricePerNight, request.Capacity, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteRoom([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await roomService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("address")]
    [Authorize]
    public async Task<ActionResult> GetRoomsByAddress([FromQuery] string address, CancellationToken cancellationToken)
    {
        var rooms = await roomService.GetAllAsync(cancellationToken);
        return Ok(rooms);
    }

    [HttpGet("available")]
    [Authorize]
    public async Task<ActionResult> GetAvailableRooms([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
    {
        var rooms = await roomService.GetAvailableRoomsAsync(startDate, endDate, cancellationToken);
        return Ok(rooms);
    }
}