using AutoMapper;
using BookingSystemApi.Application.Dto;
using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Application.Mapping;

public class BookingSystemProfile : Profile
{
    public BookingSystemProfile()
    {
        CreateMap<HotelEntity, HotelDto>();
        CreateMap<RoomEntity, RoomDto>();
        CreateMap<BookingEntity, BookingDto>();
    }
}