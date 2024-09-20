using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SharedKernel;
using ElevatorChallenge.Core.Entities;
using static ElevatorChallenge.Core.Enums.Directions;

namespace ElevatorChallenge.Core.ContributorAggregate.Events;
public  class MoveToFloorEvent : DomainEventBase
{

  public int ElevatorId { get; set; }
  public int CurrentFloor { get; set; }
  public int TargetFloor { get; set; }
  public Elevator Elevator { get; set; }
  public int NumberOfPeople { get; set; }
  public int PeopleWaiting { get; set; }


  public Entities.Direction CurrentDirection { get; set; }


  public MoveToFloorEvent(Elevator elevator, int targetFloor)
  {
    TargetFloor = targetFloor;
    Elevator = elevator;

  }

}
