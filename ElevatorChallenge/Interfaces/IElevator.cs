namespace ElevatorChallenge.Models
{
    public interface IElevator
    {
     
        void LoadPeople(int numberOfPeople);
        void MoveToFloor(int targetFloor);
        void UnloadPeople(int numberOfPeople);
    }
}