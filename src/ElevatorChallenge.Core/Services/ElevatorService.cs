using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;
using Ardalis.SharedKernel;
using MediatR;
using ElevatorChallenge.Core.ContributorAggregate.Events;


namespace ElevatorChallenge.Core.Services;
public class ElevatorService : IElevatorService
{
  private readonly IElevatorSelectionStrategy _elevatorSelectionStrategy;
  public  List<Elevator> _elevators;
  private readonly IMediator _mediator;

  public ElevatorService(IElevatorSelectionStrategy strategy, List<Elevator> elevators, IMediator mediator)
  {
    _elevatorSelectionStrategy = strategy;
    _elevators = elevators;
    _mediator = mediator;
  }

  public async Task RequestElevatorAsync(int floor, int peopleWaiting, int peopleToUnload)
  {
    try
    {
      var selectedElevator = await _elevatorSelectionStrategy.SelectElevator(_elevators, floor);

      if (selectedElevator != null)
      {
        await _mediator.Publish(new MoveToFloorEvent(selectedElevator, floor));

        //another possible way to do this would be to have each function have its own event and handler and publish its own event 
        await _mediator.Publish(new LoadPeopleEvent(selectedElevator, peopleWaiting));

        //await _mediator.Publish(new UnloadPeopleEvent(selectedElevator.elevatorId, selectedElevator.PeopleInElevator, peopleWaiting));
      }
      else
      {
        Console.WriteLine("There are no Elevators Currently");
      }
    }
    catch (Exception Ex)
    {
      Console.WriteLine(Ex.Message);
    }

  }

  public void DisplayElevatorStatus()
  {
    foreach (var elevator in _elevators)
    {
      Console.WriteLine($"Elevator {elevator.elevatorId}: Current Floor: {elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, People: {elevator.PeopleInElevator}/{elevator.Capacity}");
    }
  }
}

