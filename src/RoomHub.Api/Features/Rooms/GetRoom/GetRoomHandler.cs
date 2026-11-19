using RoomHub.Api.Common;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(RoomRepository rooms)
    : ICommandHandler<GetRoomCommand, GetRoomResponse>
{
    public Task<Result<GetRoomResponse>> Handle(GetRoomCommand command, CancellationToken ct)
    {
        var room = rooms.Get(command.Id);
        if (room is null)
            return Task.FromResult(Result<GetRoomResponse>.Failure(RoomErrors.RoomNotFound));

        return Task.FromResult<Result<GetRoomResponse>>(
            new GetRoomResponse(room.Id, room.Name, room.Capacity));
    }
}
