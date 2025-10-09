using BookingSystemApi.Persistence.Interfaces;
using BookingSystemApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystemApi.Persistence.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingSystemDbContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("BookingSystemDbContext"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("BookingSystemDbContext")));
        });
        
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        
        return services;
    }
}