using RoomHub.Api.Common;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository rooms, IUnitOfWork uow)
    : ICommandHandler<CreateRoomCommand, CreateRoomResponse>
{
    public async Task<Result<CreateRoomResponse>> Handle(CreateRoomCommand command, CancellationToken ct)
    {
        var creation = Room.Create(command.Name, command.Capacity);
        if (creation.IsFailure)
            return creation.Error;

        var room = creation.Value;
        rooms.Add(room);
        await uow.SaveChangesAsync(ct);

        return new CreateRoomResponse(room.Id, room.Name, room.Capacity);
    }
}
