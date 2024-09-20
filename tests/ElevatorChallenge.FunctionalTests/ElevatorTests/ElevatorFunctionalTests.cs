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

public class ElevatorFunctionalTests
{
  [Fact]
  public async Task MultipleRequests_ShouldHandleCorrectly()
  {
    // Arrange
    var elevator1 = new Elevator(10, 1);
    var elevator2 = new Elevator(10, 2);
    var elevators = new List<Elevator> { elevator1, elevator2 };

    var mockStrategy = new Mock<IElevatorSelectionStrategy>();
    var mockMediator = new Mock<IMediator>();

    // Setup strategy for elevator selection
    mockStrategy.SetupSequence(s => s.SelectElevator(It.IsAny<List<Elevator>>(), It.IsAny<int>()))
        .ReturnsAsync(elevator1)  
        .ReturnsAsync(elevator2); 

    // Setup mediator to handle publishing ElevatorMovedEvent for each request
    mockMediator.Setup(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask)
            .Callback<INotification, CancellationToken>(async (notification, token) =>
            {
              var elevatorMovedEvent = notification as MoveToFloorEvent;
          if (elevatorMovedEvent != null)
          {
            // Move the correct elevator based on the event target floor
            if (elevatorMovedEvent.Elevator == elevator1)
                  await Task.Run(() => elevator1.MoveToFloor(elevatorMovedEvent.TargetFloor)); 

            else if (elevatorMovedEvent.Elevator == elevator2)
                  await Task.Run(() => elevator2.MoveToFloor(elevatorMovedEvent.TargetFloor)); 
              }
            });

    var service = new ElevatorService(mockStrategy.Object, elevators, mockMediator.Object);

    // Act
    //two requests
    await service.RequestElevatorAsync(2, 4, 2);  
    await service.RequestElevatorAsync(7, 6, 3); 

    // Assert
    //To check if elevator moved to the coprrect floor 
    Assert.Equal(2, elevator1.CurrentFloor);
    Assert.Equal(7, elevator2.CurrentFloor);

    // to Verify if the mediator publish vent was called  
    mockMediator.Verify(m => m.Publish(It.IsAny<MoveToFloorEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
  }
}
