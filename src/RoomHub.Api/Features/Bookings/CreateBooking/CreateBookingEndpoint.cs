using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public record CreateBookingRequest(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);

public static class CreateBookingEndpoint
{
    public static void MapCreateBooking(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms/{roomId:guid}/bookings", async (
            Guid roomId,
            CreateBookingRequest request,
            ICommandHandler<CreateBookingCommand, CreateBookingResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(
                new CreateBookingCommand(
                    roomId,
                    request.BookedBy,
                    request.StartUtc,
                    request.EndUtc,
                    request.Attendees),
                ct);

            return result.IsSuccess
                ? TypedResults.Created($"/api/bookings/{result.Value.Id}", result.Value)
                : result.Error.ToProblem();
        })
        .WithTags("Bookings");
    }
}
