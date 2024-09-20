using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.Entities;

namespace ElevatorChallenge.Core.Interfaces;
public  interface IElevatorSelectionStrategy
{
  Task<Elevator?> SelectElevator(List<Elevator> elevators, int requestedFloor);

}
