using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;
using ElevatorChallenge.Core.Services;
using ElevatorChallenge.Core.ContributorAggregate.Events;

public class ElevatorServiceTests
{
  [Fact]
  public async Task RequestElevatorAsync_ShouldMoveToRequestedFloor()
  {
    // Arrange
    //future refactoring move this to the constructor so the when test run it Sets up everything that i need 

    var mockStrategy = new Mock<IElevatorSelectionStrategy>();
    var mockMediator = new Mock<IMediator>();

    var elevator = new Elevator(10, 1);
    var elevators = new List<Elevator> { elevator };

    // Setup the mock strategy to return the elevator 
    mockStrategy.Setup(s => s.SelectElevator(It.IsAny<List<Elevator>>(), It.IsAny<int>()))
        .ReturnsAsync(elevator);

    // Setup the mediator mock to handle the publication of domain events (MoveToFloorEvent)
    mockMediator.Setup(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Callback<INotification, CancellationToken>(async (notification, token) =>
            {
              var elevatorMovedEvent = notification as MoveToFloorEvent;
              if (elevatorMovedEvent != null)
              {
                await Task.Run(() => elevator.MoveToFloor(elevatorMovedEvent.TargetFloor));
              }
            });

    var service = new ElevatorService(mockStrategy.Object, elevators, mockMediator.Object);

    // Act
    await service.RequestElevatorAsync(3, 5, 2);

    // Assert
    // Ensure the elevator moved to the correct floor
    Assert.Equal(3, elevator.CurrentFloor);


    mockMediator.Verify(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()), Times.Once);
  }


  [Fact]
  public async Task RequestElevatorAsync_ShouldNotFindElevator_WhenNoneAvailable()
  {
    // Arrange
    //future refactoring move this to the constructor so the when test run it Sets up everything that i need 
    var mockStrategy = new Mock<IElevatorSelectionStrategy>();
    var elevator = new Elevator(10, 1);
    var elevators = new List<Elevator> { elevator };
    var mockMediator = new Mock<IMediator>();

    var service = new ElevatorService(mockStrategy.Object, elevators, mockMediator.Object);


    var targetFloor = 3;
    var peopleWaiting = 5;

    mockStrategy.Setup(s => s.SelectElevator(It.IsAny<List<Elevator>>(), targetFloor))
        .ReturnsAsync((Elevator?)null);

    // Act
    await service.RequestElevatorAsync(targetFloor, peopleWaiting, 0);

    // Assert
    mockMediator.Verify(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    mockMediator.Verify(m => m.Publish(It.IsAny<LoadPeopleEvent>(), It.IsAny<CancellationToken>()), Times.Never);
  }

  [Fact]
  public async Task RequestElevatorAsync_ShouldHandleException()
  {
    // Arrange
    var targetFloor = 3;
    var peopleWaiting = 5;

    //future refactoring move this to the constructor so the when test run it Sets up everything that i need 
    var mockStrategy = new Mock<IElevatorSelectionStrategy>();
    var elevator = new Elevator(10, 1);
    var elevators = new List<Elevator> { elevator };
    var mockMediator = new Mock<IMediator>();

    var service = new ElevatorService(mockStrategy.Object, elevators, mockMediator.Object);


    mockStrategy.Setup(s => s.SelectElevator(It.IsAny<List<Elevator>>(), targetFloor))
            .ThrowsAsync(new Exception("Something went wrong"));

    // Act & Assert
    var exception = await Record.ExceptionAsync(() => service.RequestElevatorAsync(targetFloor, peopleWaiting, 0));

    // Assert: Ensure no events are published when there's an exception
    mockMediator.Verify(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    mockMediator.Verify(m => m.Publish(It.IsAny<LoadPeopleEvent>(), It.IsAny<CancellationToken>()), Times.Never);

    // Check that the exception message was logged or handled properly
    Assert.Null(exception);
  }
}
