using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class BookingRepository(RoomHubDbContext db) : IBookingRepository
{
    public void Add(Booking booking) => db.Bookings.Add(booking);

    public Task<Booking?> GetAsync(Guid id, CancellationToken ct) =>
        db.Bookings.FirstOrDefaultAsync(b => b.Id == id, ct);

    public async Task<List<Booking>> ListForRoomAsync(Guid roomId, CancellationToken ct) =>
        await db.Bookings.Where(b => b.RoomId == roomId).ToListAsync(ct);

    public async Task<List<Booking>> ListActiveByBookedByOnDateAsync(
        string bookedBy,
        DateOnly date,
        CancellationToken ct)
    {
        var trimmed = bookedBy.Trim();
        var dayStart = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var dayEnd = dayStart.AddDays(1);

        return await db.Bookings
            .Where(b => b.Status == BookingStatus.Active
                && b.BookedBy == trimmed
                && b.StartUtc >= dayStart
                && b.StartUtc < dayEnd)
            .ToListAsync(ct);
    }
}
