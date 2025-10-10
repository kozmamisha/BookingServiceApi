namespace BookingSystemApi.Contracts.Booking;

public class BookingUpsertRequest
{
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}