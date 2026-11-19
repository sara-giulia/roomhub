using RoomHub.Api.Common;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public record CancelBookingCommand(Guid Id) : ICommand<Unit>;
