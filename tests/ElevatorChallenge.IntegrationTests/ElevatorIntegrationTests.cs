using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;
using ElevatorChallenge.Core.Services;
using ElevatorChallenge.Core.ContributorAggregate.Events;

public class ElevatorIntegrationTests
{
  [Fact]
  public async Task ElevatorService_ShouldHandleElevatorRequests()
  {
    // Arrange
    var elevator = new Elevator(10, 1);  
    var elevators = new List<Elevator> { elevator };

    // Mock MediatR to handle events
    var mockMediator = new Mock<IMediator>();

    // Setup mock strategy to select an elevator
    var mockStrategy = new Mock<IElevatorSelectionStrategy>();
    mockStrategy.Setup(s => s.SelectElevator(It.IsAny<List<Elevator>>(), It.IsAny<int>()))
        .ReturnsAsync(elevator);

    // Track if MoveToFloorEvent has already been triggered
    var moveToFloorExecuted = false;

    // Setup mediator to handle publishing of events (ElevatorMovedEvent and LoadPeopleEvent)
    mockMediator.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask)
        .Callback<INotification, CancellationToken>(async (notification, token) =>
        {
          if (notification is MoveToFloorEvent movedEvent && !moveToFloorExecuted)
          {
            // Ensure that this block is only executed once
            moveToFloorExecuted = true;
            await Task.Run(() => movedEvent.Elevator.MoveToFloor(movedEvent.TargetFloor));
          }
          else if (notification is LoadPeopleEvent loadPeopleEvent)
          {
            await Task.Run(() => loadPeopleEvent.Elevator.LoadPeople(loadPeopleEvent.NumberOfPeople));
          }
        
        });

    // Create the ElevatorService instance
    var service = new ElevatorService(mockStrategy.Object, elevators, mockMediator.Object);

    // Act
    await service.RequestElevatorAsync(5, 3, 1);  

    // Assert
    Assert.Equal(5, elevator.CurrentFloor);

    Assert.Equal(2, elevator.PeopleInElevator);

    mockMediator.Verify(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()), Times.Once);

    mockMediator.Verify(m => m.Publish(It.IsAny<LoadPeopleEvent>(), It.IsAny<CancellationToken>()), Times.Once);
  }
}
