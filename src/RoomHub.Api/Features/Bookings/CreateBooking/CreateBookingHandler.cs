using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork uow,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public async Task<Result<CreateBookingResponse>> Handle(
        CreateBookingCommand command,
        CancellationToken ct)
    {
        // 404 before any validation
        var room = await rooms.GetAsync(command.RoomId, ct);
        if (room is null)
            return RoomErrors.RoomNotFound;

        var nowUtc = timeProvider.GetUtcNow().UtcDateTime;
        var existing = await bookings.ListForRoomAsync(command.RoomId, ct);

        var dailyForParticipant = !string.IsNullOrWhiteSpace(command.BookedBy)
            ? await bookings.ListActiveByBookedByOnDateAsync(
                command.BookedBy,
                DateOnly.FromDateTime(command.StartUtc),
                ct)
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
            return creation.Error;

        var booking = creation.Value;
        bookings.Add(booking);
        await uow.SaveChangesAsync(ct);

        return new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status.ToString());
    }
}
