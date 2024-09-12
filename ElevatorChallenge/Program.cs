using ElevatorChallenge.Factory;
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Models;
using ElevatorChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var elevators = new List<Elevator>
        {
            ElevatorFactory.CreateElevator(5),
            ElevatorFactory.CreateElevator(8),
            ElevatorFactory.CreateElevator(10)
        };

            // Choose strategy
            Console.WriteLine("Select elevator dispatch strategy:");
            Console.WriteLine("1. Nearest Elevator");
            Console.WriteLine("2. Least Busy Elevator");
            int choice = int.Parse(Console.ReadLine());

            var strategyFactory = ElevatorSelectionFactory.CreateStrategy(choice);        

            // Inject strategy and elevators into the service
            ElevatorService elevatorService = new ElevatorService(strategyFactory, elevators);

            // Simulate elevator requests
            while (true)
            {
                Console.WriteLine("Enter the floor to request an elevator (or type 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                int requestedFloor = int.Parse(input);
                Console.WriteLine("Enter the number of people waiting:");
                int peopleWaiting = int.Parse(Console.ReadLine());

                elevatorService.RequestElevator(requestedFloor, peopleWaiting);
            }
        }
    }
}
