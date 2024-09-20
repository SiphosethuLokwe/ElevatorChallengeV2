using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.ContributorAggregate.Events;
using MediatR;

namespace ElevatorChallenge.Core.ContributorAggregate.Handlers;
public class LoadPeopleEventHandler : INotificationHandler<LoadPeopleEvent>
{
  public async Task Handle(LoadPeopleEvent notification, CancellationToken cancellationToken)
  {
     await notification.Elevator.LoadPeople(notification.NumberOfPeople);
  }
}
