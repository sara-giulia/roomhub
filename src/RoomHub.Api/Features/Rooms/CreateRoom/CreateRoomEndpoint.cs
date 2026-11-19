using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public record CreateRoomRequest(string? Name, int Capacity);

public static class CreateRoomEndpoint
{
    public static void MapCreateRoom(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms", async (
            CreateRoomRequest request,
            ICommandHandler<CreateRoomCommand, CreateRoomResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(
                new CreateRoomCommand(request.Name, request.Capacity), ct);

            return result.IsSuccess
                ? TypedResults.Created($"/api/rooms/{result.Value.Id}", result.Value)
                : result.Error.ToProblem();
        })
        .WithTags("Rooms");
    }
}
