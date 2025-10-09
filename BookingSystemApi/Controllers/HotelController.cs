using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Contracts.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    
    public async Task<ActionResult> CreateHotel([FromBody] HotelUpsertRequest request, CancellationToken cancellationToken)
    {
        await hotelService.CreateAsync(request.Name, request.Address, request.Description, cancellationToken);
        return Created();
    }
    
    [HttpGet()]
    [Authorize]
    public async Task<ActionResult> GetAllHotels(CancellationToken cancellationToken)
    {
        var hotels = await hotelService.GetAllAsync(cancellationToken);
        return Ok(hotels);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOneHotel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var hotel = await hotelService.GetByIdAsync(id, cancellationToken);
        return Ok(hotel);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateHotel([FromRoute] Guid id, [FromBody] HotelUpsertRequest request, CancellationToken cancellationToken)
    {
        await hotelService.UpdateAsync(id, request.Name, request.Address, request.Description, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteHotel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await hotelService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}