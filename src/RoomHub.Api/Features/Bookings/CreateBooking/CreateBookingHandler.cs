using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    RoomRepository rooms,
    BookingRepository bookings,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public Task<Result<CreateBookingResponse>> Handle(
        CreateBookingCommand command,
        CancellationToken ct)
    {
        // 404 before any validation
        var room = rooms.Get(command.RoomId);
        if (room is null)
            return Task.FromResult(Result<CreateBookingResponse>.Failure(RoomErrors.RoomNotFound));

        var nowUtc = timeProvider.GetUtcNow().UtcDateTime;
        var existing = bookings.ListForRoom(command.RoomId);

        var dailyForParticipant = !string.IsNullOrWhiteSpace(command.BookedBy)
            ? bookings.ListActiveByBookedByOnDate(
                command.BookedBy,
                DateOnly.FromDateTime(command.StartUtc))
            : new List<Booking>();

        var creation = Booking.Create(
            room,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            nowUtc,
            existing,
            dailyForParticipant);

        if (creation.IsFailure)
            return Task.FromResult(Result<CreateBookingResponse>.Failure(creation.Error));

        var booking = creation.Value;
        bookings.Add(booking);

        return Task.FromResult<Result<CreateBookingResponse>>(
            new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status.ToString()));
    }
}
