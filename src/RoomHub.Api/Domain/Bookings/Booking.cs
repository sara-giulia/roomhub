using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public enum BookingStatus
{
    Active,
    Cancelled
}

public sealed class Booking
{
    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(4);

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    private Booking() { }

    private Booking(
        Guid id,
        Guid roomId,
        string bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees)
    {
        Id = id;
        RoomId = roomId;
        BookedBy = bookedBy;
        StartUtc = startUtc;
        EndUtc = endUtc;
        Attendees = attendees;
        Status = BookingStatus.Active;
    }

    public static Result<Booking> Create(
        Room room,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc,
        IEnumerable<Booking> existingForRoom,
        IEnumerable<Booking> existingForParticipantOnDay)
    {
        // R2
        if (string.IsNullOrWhiteSpace(bookedBy))
            return BookingErrors.InvalidBookedBy;

        // R3
        if (endUtc <= startUtc)
            return BookingErrors.InvalidRange;

        // R4
        if (attendees <= 0)
            return BookingErrors.InvalidAttendees;

        if (attendees > room.Capacity)
            return BookingErrors.AttendeesExceedCapacity;

        // R5
        if (startUtc < nowUtc)
            return BookingErrors.StartInPast;

        // R6
        if (endUtc - startUtc > MaxDuration)
            return BookingErrors.DurationTooLong;

        // R7 - only Active bookings count; touching intervals do not overlap
        foreach (var other in existingForRoom)
        {
            if (other.Status != BookingStatus.Active)
                continue;

            if (startUtc < other.EndUtc && other.StartUtc < endUtc)
                return BookingErrors.RoomOccupied;
        }

        // R9 - at most 2 active bookings per day per participant, across all rooms
        var trimmedBookedBy = bookedBy.Trim();
        var startDate = DateOnly.FromDateTime(startUtc);
        var activeOnDay = 0;
        foreach (var other in existingForParticipantOnDay)
        {
            if (other.Status != BookingStatus.Active)
                continue;
            if (other.BookedBy != trimmedBookedBy)
                continue;
            if (DateOnly.FromDateTime(other.StartUtc) != startDate)
                continue;

            activeOnDay++;
            if (activeOnDay >= 2)
                return BookingErrors.DailyLimitExceeded;
        }

        return new Booking(Guid.NewGuid(), room.Id, trimmedBookedBy, startUtc, endUtc, attendees);
    }

    // R8
    public Result<Unit> Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return BookingErrors.AlreadyCancelled;

        Status = BookingStatus.Cancelled;
        return Unit.Value;
    }
}
