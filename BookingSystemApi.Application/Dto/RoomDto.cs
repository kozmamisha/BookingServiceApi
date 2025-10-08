namespace BookingSystemApi.Application.Dto;

public class RoomDto
{
    public Guid Id { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public Guid HotelId { get; set; }
}