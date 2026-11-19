using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(BookingRepository bookings)
    : ICommandHandler<GetBookingCommand, GetBookingResponse>
{
    public Task<Result<GetBookingResponse>> Handle(GetBookingCommand command, CancellationToken ct)
    {
        var booking = bookings.Get(command.Id);
        if (booking is null)
            return Task.FromResult(Result<GetBookingResponse>.Failure(BookingErrors.BookingNotFound));

        return Task.FromResult<Result<GetBookingResponse>>(
            new GetBookingResponse(
                booking.Id,
                booking.RoomId,
                booking.BookedBy,
                booking.StartUtc,
                booking.EndUtc,
                booking.Attendees,
                booking.Status.ToString()));
    }
}
