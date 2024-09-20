using ElevatorChallenge.Core.Entities;

namespace ElevatorChallenge.Core.Interfaces;

public interface IElevatorService
{
  Task RequestElevatorAsync(int floor, int peopleWaiting, int peopleToUnload);
  void DisplayElevatorStatus();
}
