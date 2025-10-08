namespace BookingSystemApi.Contracts.Hotel;

public class RoomCreateRequest
{
    public decimal PricePerNight { get; set; }
    public int  Capacity { get; set; }
    public Guid HotelId { get; set; }
}