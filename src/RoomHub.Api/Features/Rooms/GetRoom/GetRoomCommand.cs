using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public record GetRoomCommand(Guid Id) : ICommand<GetRoomResponse>;

public record GetRoomResponse(Guid Id, string Name, int Capacity);
