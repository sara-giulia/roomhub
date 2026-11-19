using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error InvalidBookedBy =
        new("InvalidBookedBy", "BookedBy is required.", ErrorType.Validation);

    public static readonly Error InvalidRange =
        new("InvalidRange", "The end must be after the start.", ErrorType.Validation);

    public static readonly Error InvalidAttendees =
        new("InvalidAttendees", "Attendees must be positive.", ErrorType.Validation);

    public static readonly Error AttendeesExceedCapacity =
        new("AttendeesExceedCapacity", "The room does not have enough seats.", ErrorType.Validation);

    public static readonly Error StartInPast =
        new("StartInPast", "A booking cannot start in the past.", ErrorType.Validation);

    public static readonly Error DurationTooLong =
        new("DurationTooLong", "The booking cannot exceed 4 hours.", ErrorType.Validation);

    public static readonly Error RoomOccupied =
        new("RoomOccupied", "The room is occupied in this interval.", ErrorType.Conflict);

    public static readonly Error AlreadyCancelled =
        new("AlreadyCancelled", "The booking is already cancelled.", ErrorType.Conflict);

    public static readonly Error DailyLimitExceeded =
        new("DailyLimitExceeded", "A participant cannot have more than 2 active bookings per day.", ErrorType.Conflict);

    public static readonly Error BookingNotFound =
        new("BookingNotFound", "The booking does not exist.", ErrorType.NotFound);
}
