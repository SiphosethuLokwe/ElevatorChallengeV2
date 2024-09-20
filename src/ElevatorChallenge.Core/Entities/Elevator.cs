using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SharedKernel;
using ElevatorChallenge.Core.ContributorAggregate.Events;
using static ElevatorChallenge.Core.Enums.Directions;

namespace ElevatorChallenge.Core.Entities;
public enum Direction
{
  Up,
  Down,
  Idle
}
public class Elevator 
{
  public Elevator() { }
  public int elevatorId { get; set; }
  public int CurrentFloor { get; set; }
  public Direction CurrentDirection { get; set; }
  public int PeopleInElevator { get; set; }
  public int Capacity { get; set; }


  public Elevator(int capacity, int id )
  {
    Capacity = capacity;
    CurrentFloor = 0;
    CurrentDirection = Direction.Idle;
    PeopleInElevator = 0;
    elevatorId = id;
  }

  public async Task  MoveToFloor(int targetFloor)
  {
    /*this method keeps track at where the elevator is moving, and then sets the floor to be the requested floor,
     * else if the same floor where the elevator is then its idle(same floor requested) also once the target floor has been reahced it becomes
     * idle*/
    try
    {
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
    catch (Exception ex)
    {
      Console.WriteLine(ex.ToString());
    }
    await Task.CompletedTask;

  }

  public async Task LoadPeople(int numberOfPeople)
  {
    /*This method keeps track of the number of people that have been loaded onto the elevator */
    try
    {
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
    catch (Exception ex)
    {
      Console.WriteLine(ex.ToString());
    }
    await Task.CompletedTask;
  }

  public async Task UnloadPeople(int numberOfPeople)
  {
    try
    {
      if (PeopleInElevator >= numberOfPeople)
      {
        PeopleInElevator -= numberOfPeople;
      }
      else
      {
        Console.WriteLine("Not enough people in the elevator to unload.");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine (ex.ToString());
    }
    await Task.CompletedTask;
  }

  public bool IsAvailable => CurrentDirection == Direction.Idle;
}
