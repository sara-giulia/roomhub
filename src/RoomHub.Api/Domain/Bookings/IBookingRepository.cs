namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    void Add(Booking booking);
    Task<Booking?> GetAsync(Guid id, CancellationToken ct);
    Task<List<Booking>> ListForRoomAsync(Guid roomId, CancellationToken ct);
    Task<List<Booking>> ListActiveByBookedByOnDateAsync(string bookedBy, DateOnly date, CancellationToken ct);
}
