using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public record CreateRoomCommand(string? Name, int Capacity) : ICommand<CreateRoomResponse>;

public record CreateRoomResponse(Guid Id, string Name, int Capacity);
