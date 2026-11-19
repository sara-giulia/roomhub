using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoom(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rooms/{id:guid}", async (
            Guid id,
            ICommandHandler<GetRoomCommand, GetRoomResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetRoomCommand(id), ct);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : result.Error.ToProblem();
        })
        .WithTags("Rooms");
    }
}
