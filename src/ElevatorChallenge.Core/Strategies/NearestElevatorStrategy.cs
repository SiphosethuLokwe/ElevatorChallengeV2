using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Core.Strategies;
internal class NearestElevatorStrategy : IElevatorSelectionStrategy
{
  public Task<Elevator? >SelectElevator(List<Elevator> elevators, int requestedFloor)
  {
    Elevator? closestElevator = null;
    int minDistance = int.MaxValue;

    // Single pass to find the elevator with the minimum distance to the requested floor
    foreach (var elevator in elevators)
    {
      if (elevator.IsAvailable)
      {
        int distance = Math.Abs(elevator.CurrentFloor - requestedFloor);
        if (distance < minDistance)
        {
          minDistance = distance;
          closestElevator = elevator;
        }
      }
    }

    return Task.FromResult<Elevator?>(closestElevator);
  }
  
}
