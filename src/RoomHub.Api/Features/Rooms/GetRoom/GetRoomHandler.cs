using RoomHub.Api.Common;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(IRoomRepository rooms)
    : ICommandHandler<GetRoomCommand, GetRoomResponse>
{
    public async Task<Result<GetRoomResponse>> Handle(GetRoomCommand command, CancellationToken ct)
    {
        var room = await rooms.GetAsync(command.Id, ct);
        if (room is null)
            return RoomErrors.RoomNotFound;

        return new GetRoomResponse(room.Id, room.Name, room.Capacity);
    }
}
