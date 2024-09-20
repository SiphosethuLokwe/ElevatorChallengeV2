using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Core.Strategies;
public class LeastBusyElevatorStrategy : IElevatorSelectionStrategy
{
  public  Task<Elevator?> SelectElevator(List<Elevator> elevators, int requestedFloor)
  {
    Elevator? leastBusyElevator = null; // Allow null assignment
    int minPeopleInElevator = int.MaxValue;

    foreach (var elevator in elevators)
    {
      if (elevator.IsAvailable && elevator.PeopleInElevator < minPeopleInElevator)
      {
        minPeopleInElevator = elevator.PeopleInElevator;
        leastBusyElevator = elevator;
      }
    }

    return Task.FromResult<Elevator?>(leastBusyElevator);
  }
}
