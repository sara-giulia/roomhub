using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public record CreateBookingCommand(
    Guid RoomId,
    string? BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees) : ICommand<CreateBookingResponse>;

public record CreateBookingResponse(Guid Id, Guid RoomId, string Status);
