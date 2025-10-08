namespace BookingSystemApi.Application.Dto;

public class HotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public ICollection<RoomDto>? Rooms { get; set; }
}