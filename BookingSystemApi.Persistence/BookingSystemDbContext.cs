using BookingSystemApi.Core.Entities;
using BookingSystemApi.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSystemApi.Persistence;

public class BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options)
    : IdentityDbContext<UserEntity>(options)
{
    public DbSet<BookingEntity> Bookings { get; set; }
    public DbSet<HotelEntity> Hotels { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new HotelConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}