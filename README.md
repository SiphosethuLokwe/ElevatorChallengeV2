Elevator Challenge - Clean Architecture using MediatR
Project Overview
The Elevator Challenge project demonstrates the application of the MediatR pattern within a Clean Architecture structure. This project simulates the behavior of an elevator system, allowing requests to be made for elevators to move to a specific floor, load and unload passengers, and handle multiple requests concurrently.

The project is based on Ardalis Clean Architecture principles, leveraging MediatR for implementing domain events and request handling to promote decoupling and scalability.

Why Use the MediatR Pattern?
1. Separation of Concerns:
MediatR helps maintain a clean separation of concerns by ensuring that business logic, event handling, and service orchestration are separated from the core domain logic. Instead of tightly coupling services and objects to event handlers, MediatR acts as a mediator to handle interactions between them.

2. Decoupling:
The MediatR pattern decouples the sender of a request from its receiver. This means the ElevatorService does not need to know which class handles the logic for moving elevators or loading/unloading passengers. Instead, these responsibilities are handed off to domain event handlers using MediatR, leading to highly maintainable code.

3. Domain Events:
Using MediatR to manage domain events allows us to dispatch and handle events asynchronously, such as:

Moving an elevator (MoveToFloorEvent).
Loading and unloading passengers (LoadPeopleEvent, UnloadPeopleEvent).
MediatR ensures these events are published and processed without the elevator needing direct knowledge of the event handler logic.

4. Scalability and Extensibility:
With MediatR, you can easily add new event handlers (e.g., handling new types of elevator requests) without modifying the core service logic. This enables you to extend the system with minimal code changes.

Key Features
ElevatorService: Manages elevator operations including moving, loading, and unloading.
MediatR: Handles domain events to coordinate elevator movements and passenger operations.
Event Handling: Implements domain events for elevator actions (move, load, unload).
Elevator Selection Strategies: Uses different strategies for selecting the most suitable elevator.
Unit, Functional, and Integration Tests: Verifies the correctness of the elevator system using Xunit and Moq.


Project Structure

ElevatorChallenge
│   ├── Core
│   │   ├── Entities
│   │   ├── Interfaces
│   │   └── Events
│   ├── Infrastructure
│   │   └── DomainEventDispatcher.cs
│   ├── Application
│   └── Tests
│       ├── UnitTests
│       ├── FunctionalTests
│       └── IntegrationTests
└── README.md

Event Flow
Elevator Request: When an elevator is requested, the ElevatorService selects an appropriate elevator using the strategy.
MoveToFloorEvent: The elevator triggers a MoveToFloorEvent via MediatR to simulate moving to the requested floor.
LoadPeopleEvent/UnloadPeopleEvent: Once the elevator reaches the floor, it triggers events for loading and unloading people.

Test Coverage
The project includes comprehensive tests:

Unit Tests: Verifying individual behaviors of elevator service, strategies, and event handling.
Functional Tests: Testing how multiple elevators handle various scenarios with passenger movement.
Integration Tests: Ensuring the overall elevator system works correctly with event-driven communication.
