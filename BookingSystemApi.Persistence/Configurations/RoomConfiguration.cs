using BookingSystemApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystemApi.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.PricePerNight)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(r => r.Capacity)
            .IsRequired();
        
        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);

        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId);
    }
}