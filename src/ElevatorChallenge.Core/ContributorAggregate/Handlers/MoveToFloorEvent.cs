using ElevatorChallenge.Core.ContributorAggregate.Events;
using ElevatorChallenge.Core.Services;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class MoveToFloorEventHandler : INotificationHandler<MoveToFloorEvent>
{

    public async Task Handle(MoveToFloorEvent notification, CancellationToken cancellationToken)
    {
        await notification.Elevator.MoveToFloor(notification.TargetFloor);
    }
}
