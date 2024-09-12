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

            // Choose strategy at which to call elevator 
            Console.WriteLine("Select elevator dispatch strategy:");
            Console.WriteLine("1. Nearest Elevator");
            Console.WriteLine("2. Least Busy Elevator");
            int choice = int.Parse(Console.ReadLine());

            var strategyFactory = ElevatorSelectionFactory.CreateStrategy(choice);        

            // This inject the startegy and the number of elevators available , based on which strategy you select , the elevator will be called 
            ElevatorService elevatorService = new ElevatorService(strategyFactory, elevators);

            // This is just to simulate the request at this point 
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
