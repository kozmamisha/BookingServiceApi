﻿namespace BookingSystemApi.Core.Entities;

public class HotelEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public ICollection<RoomEntity> Rooms { get; set; } = new List<RoomEntity>();
}