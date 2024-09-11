using ElevatorChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Models
{
    public class LeastBusyElevatorStrategy : IElevatorSelectionStrategy
    {
        public Elevator SelectElevator(List<Elevator> elevators, int requestedFloor)
        {
            //This method is meant to get the elevator based on whether it is busy or not simple eneough 
            return elevators
                .Where(e => e.IsAvailable)
                .OrderBy(e => e.PeopleInElevator)
                .FirstOrDefault();
        }
    }
}
