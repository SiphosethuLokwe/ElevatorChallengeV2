using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorChallenge.Infrastructure;
public class DomainEventDispatcher : IDomainEventDispatcher
{
  private readonly IMediator _mediator;

  public DomainEventDispatcher(IMediator mediator)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  // Dispatches and clears events for non-generic entities
  public async Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents)
  {
    foreach (var entity in entitiesWithEvents)
    {
      var domainEvents = entity.DomainEvents.ToList();  // Get a copy of the events list

      // Dispatch each event through MediatR
      foreach (var domainEvent in domainEvents)
      {
        await _mediator.Publish(domainEvent);
      }
    }
  }

  // Dispatches and clears events for generic entities with a specific TId
  public async Task DispatchAndClearEvents<TId>(IEnumerable<EntityBase<TId>> entitiesWithEvents) where TId : struct, IEquatable<TId>
  {
    foreach (var entity in entitiesWithEvents)
    {
      var domainEvents = entity.DomainEvents.ToList();  // Get a copy of the events list

      // Dispatch each event through MediatR
      foreach (var domainEvent in domainEvents)
      {
        await _mediator.Publish(domainEvent);
      }
    }
  }
}
