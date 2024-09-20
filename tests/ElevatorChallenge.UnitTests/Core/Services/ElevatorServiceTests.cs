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
}
