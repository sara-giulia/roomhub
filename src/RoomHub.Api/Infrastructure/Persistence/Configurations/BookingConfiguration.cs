using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();

        builder.Property(b => b.RoomId);
        builder.Property(b => b.BookedBy).IsRequired();

        var utcConverter = new UtcDateTimeConverter();
        builder.Property(b => b.StartUtc).HasConversion(utcConverter);
        builder.Property(b => b.EndUtc).HasConversion(utcConverter);

        builder.Property(b => b.Attendees);

        builder.Property(b => b.Status).HasConversion<string>();

        builder.HasIndex(b => b.RoomId);
        builder.HasIndex(b => new { b.BookedBy, b.StartUtc });
    }
}
