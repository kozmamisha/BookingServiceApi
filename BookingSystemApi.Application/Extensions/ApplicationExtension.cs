using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystemApi.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBookingService, BookingService>();
        serviceCollection.AddScoped<IHotelService, HotelService>();
        serviceCollection.AddScoped<IRoomService, RoomService>();
        serviceCollection.AddScoped<IUserService, UserService>();

        return serviceCollection;
    }
}