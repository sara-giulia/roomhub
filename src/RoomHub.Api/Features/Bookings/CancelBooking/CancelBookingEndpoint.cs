using RoomHub.Api.Services;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBooking(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", (Guid id, BookingService service) =>
        {
            service.Cancel(id);
            return Results.NoContent();
        })
        .WithTags("Bookings");
    }
}
