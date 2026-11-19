using RoomHub.Api.Common;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBooking(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", async (
            Guid id,
            ICommandHandler<CancelBookingCommand, Unit> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(new CancelBookingCommand(id), ct);

            return result.IsSuccess
                ? TypedResults.NoContent()
                : result.Error.ToProblem();
        })
        .WithTags("Bookings");
    }
}
