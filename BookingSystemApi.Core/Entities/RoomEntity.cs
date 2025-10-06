namespace BookingSystemApi.Core.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    
    public ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
    
    public Guid HotelId { get; set; }
    public HotelEntity? Hotel { get; set; }
}