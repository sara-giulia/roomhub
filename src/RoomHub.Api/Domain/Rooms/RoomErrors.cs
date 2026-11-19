using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static readonly Error InvalidName =
        new("InvalidName", "Room name is required.", ErrorType.Validation);

    public static readonly Error InvalidCapacity =
        new("InvalidCapacity", "Capacity must be positive.", ErrorType.Validation);

    public static readonly Error RoomNotFound =
        new("RoomNotFound", "The room does not exist.", ErrorType.NotFound);
}
