namespace ElevatorChallenge.Services
{
    public interface IElevatorService
    {
        void RequestElevator(int floor, int peopleWaiting);
    }
}