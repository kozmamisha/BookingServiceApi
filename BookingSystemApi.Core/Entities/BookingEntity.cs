namespace BookingSystemApi.Core.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }
    
    public Guid RoomId { get; set; }
    public RoomEntity? Room { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}