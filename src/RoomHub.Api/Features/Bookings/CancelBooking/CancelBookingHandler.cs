using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler(
    IBookingRepository bookings,
    IUnitOfWork uow,
    TimeProvider timeProvider)
    : ICommandHandler<CancelBookingCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CancelBookingCommand command, CancellationToken ct)
    {
        var booking = await bookings.GetAsync(command.Id, ct);
        if (booking is null)
            return BookingErrors.BookingNotFound;

        var nowUtc = timeProvider.GetUtcNow().UtcDateTime;
        var cancelled = booking.Cancel(nowUtc);
        if (cancelled.IsFailure)
            return cancelled.Error;

        await uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
