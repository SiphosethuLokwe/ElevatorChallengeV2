using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Interfaces
{
    public interface IElevatorSelectionStrategy
    {
        Elevator SelectElevator(List<Elevator> elevators, int requestedFloor);

    }
}
