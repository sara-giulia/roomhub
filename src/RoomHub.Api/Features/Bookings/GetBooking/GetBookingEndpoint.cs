using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBooking(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings/{id:guid}", async (
            Guid id,
            ICommandHandler<GetBookingCommand, GetBookingResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetBookingCommand(id), ct);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : result.Error.ToProblem();
        })
        .WithTags("Bookings");
    }
}
