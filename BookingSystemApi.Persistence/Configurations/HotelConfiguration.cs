using BookingSystemApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystemApi.Persistence.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<HotelEntity>
{
    public void Configure(EntityTypeBuilder<HotelEntity> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Address)
            .IsRequired();

        builder.Property(h => h.Description)
            .HasMaxLength(1000);

        builder.HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);
    }
}