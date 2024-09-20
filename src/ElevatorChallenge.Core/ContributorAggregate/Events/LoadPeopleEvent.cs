using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SharedKernel;
using ElevatorChallenge.Core.Entities;

namespace ElevatorChallenge.Core.ContributorAggregate.Events;
public  class LoadPeopleEvent : DomainEventBase
{
  public Elevator Elevator { get; set; }

  public int NumberOfPeople { get; set; }

  public LoadPeopleEvent(Elevator elevator , int numberOfPeople)
  {
    Elevator = elevator;
    NumberOfPeople = numberOfPeople;  
  }





}
