namespace RoomHub.Api.Domain.Rooms;

public interface IRoomRepository
{
    void Add(Room room);
    Task<Room?> GetAsync(Guid id, CancellationToken ct);
}
