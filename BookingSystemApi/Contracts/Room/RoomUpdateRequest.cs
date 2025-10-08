namespace BookingSystemApi.Contracts.Hotel;

public class RoomUpdateRequest
{
    public decimal PricePerNight { get; set; }
    public int  Capacity { get; set; }
}