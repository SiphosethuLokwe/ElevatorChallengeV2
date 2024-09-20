using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Core.ContributorAggregate.Events;
using ElevatorChallenge.Core.ContributorAggregate.Handlers;
using ElevatorChallenge.Core.Entities;
using Moq;
using Xunit;

namespace ElevatorChallenge.UnitTests.Core.ContributorAggregate;
public class ElevatorHandlerTest
{
  [Fact]
  public async Task ElevatorHanlerTest_ShouldMoveToFloor()
  {
    //Arange
    var elevator = new Elevator(10, 1); // Create a real Elevator instance
    var targetFloor = 5;

    // Instantiate the MoveToFloorEvent with the correct parameters
    var moveToFloorEvent = new MoveToFloorEvent(elevator, targetFloor);

    var handler = new MoveToFloorEventHandler();

    // Act
    await handler.Handle(moveToFloorEvent, CancellationToken.None);

    // Assert
    Assert.Equal(5, elevator.CurrentFloor); // Check if the elevator moved to the correct floor
    Assert.Equal(Direction.Idle, elevator.CurrentDirection);

  }

  [Fact]
  public async Task ElevatorHanlerTest_ShouldLoadPeople()
  {
    //Arange
    var elevator = new Elevator(10, 1);
    var numberofPeople = 5;

    var loadPeopleEvent = new LoadPeopleEvent(elevator, numberofPeople);

    var handler = new LoadPeopleEventHandler();

    // Act
    await handler.Handle(loadPeopleEvent, CancellationToken.None);

    // Assert
    Assert.Equal(5, elevator.PeopleInElevator);
    Assert.Equal(Direction.Idle, elevator.CurrentDirection);

  }


}
