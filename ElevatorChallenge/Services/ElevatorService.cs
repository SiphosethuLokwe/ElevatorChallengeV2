using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Services
{
    public class ElevatorService
    {
        private readonly IElevatorSelectionStrategy _elevatorSelectionStrategy;
        private readonly List<Elevator> _elevators;

        public ElevatorService(IElevatorSelectionStrategy strategy, List<Elevator> elevators)
        {
            _elevatorSelectionStrategy = strategy;
            _elevators = elevators;
        }

        public void RequestElevator(int floor, int peopleWaiting)
        {
            var selectedElevator = _elevatorSelectionStrategy.SelectElevator(_elevators, floor);

            if (selectedElevator != null)
            {
                selectedElevator.MoveToFloor(floor);
                selectedElevator.LoadPeople(peopleWaiting);
            }
            else
            {
                Console.WriteLine("No available elevators at the moment.");
            }
        }
    }

}
