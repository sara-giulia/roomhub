using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(IBookingRepository bookings)
    : ICommandHandler<GetBookingCommand, GetBookingResponse>
{
    public async Task<Result<GetBookingResponse>> Handle(GetBookingCommand command, CancellationToken ct)
    {
        var booking = await bookings.GetAsync(command.Id, ct);
        if (booking is null)
            return BookingErrors.BookingNotFound;

        return new GetBookingResponse(
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.StartUtc,
            booking.EndUtc,
            booking.Attendees,
            booking.Status.ToString());
    }
}
