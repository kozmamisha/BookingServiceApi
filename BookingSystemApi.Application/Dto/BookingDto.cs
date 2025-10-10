namespace BookingSystemApi.Application.Dto;

public class BookingDto
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public Guid RoomId { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}