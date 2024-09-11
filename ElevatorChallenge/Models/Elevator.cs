using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Models
{
    public enum Direction
    {
        Up,
        Down,
        Idle
    }
    public class Elevator 
    {
        public int CurrentFloor { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public int PeopleInElevator { get; private set; }
        public int Capacity { get; private set; }


        public Elevator(int capacity)
        {
            Capacity = capacity;
            CurrentFloor = 0;
            CurrentDirection = Direction.Idle;
            PeopleInElevator = 0;
        }

        public void MoveToFloor(int targetFloor)
        {
            /*this method keeps track at where the elevator is moving, and then sets the floor to be the requested floor,
             * where then the direction is set to iddle indicating the elevator has stopped on that floor */
            if (targetFloor > CurrentFloor)
                CurrentDirection = Direction.Up;
            else if (targetFloor < CurrentFloor)
                CurrentDirection = Direction.Down;
            else
                CurrentDirection = Direction.Idle;

            Console.WriteLine($"Elevator moving from {CurrentFloor} to {targetFloor}.");
            CurrentFloor = targetFloor;
            CurrentDirection = Direction.Idle;
        }

        public void LoadPeople(int numberOfPeople)
        {
            /*This method keeps track of the number of people that have been loaded onto the elevator */

            if (PeopleInElevator + numberOfPeople <= Capacity)
            {
                PeopleInElevator += numberOfPeople;
                Console.WriteLine($"{numberOfPeople} people entered the elevator.");
            }
            else
            {
                Console.WriteLine("Elevator is full. Cannot load more people.");
            }
        }

        public void UnloadPeople(int numberOfPeople)
        {
            /*Opposite of the previous method */


            if (PeopleInElevator >= numberOfPeople)
            {
                PeopleInElevator -= numberOfPeople;
                Console.WriteLine($"{numberOfPeople} people left the elevator.");
            }
            else
            {
                Console.WriteLine("Not enough people in the elevator to unload.");
            }
        }

        public bool IsAvailable => CurrentDirection == Direction.Idle;
    }


}
