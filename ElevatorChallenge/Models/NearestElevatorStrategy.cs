using ElevatorChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Models
{
    public class NearestElevatorStrategy : IElevatorSelectionStrategy
    {
        public Elevator SelectElevator(List<Elevator> elevators, int requestedFloor)
        {
            //so this method is esentially to get the nearest elevator by calculating the floors between the current floor and the requested floor
            //and based on this it takes the elevator that is the closest.
            return elevators
                .Where(e => e.IsAvailable)
                .OrderBy(e => Math.Abs(e.CurrentFloor - requestedFloor))
                .FirstOrDefault();
        }
    }
}
