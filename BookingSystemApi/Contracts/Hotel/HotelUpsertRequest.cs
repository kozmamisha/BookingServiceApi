﻿namespace BookingSystemApi.Contracts.Hotel;

public class HotelUpsertRequest
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}