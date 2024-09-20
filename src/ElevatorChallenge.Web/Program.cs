using Ardalis.SharedKernel;
using ElevatorChallenge.Core.ContributorAggregate.Events;
using ElevatorChallenge.Core.ContributorAggregate.Handlers;
using ElevatorChallenge.Core.Entities;
using ElevatorChallenge.Core.Interfaces;
using ElevatorChallenge.Core.Services;
using ElevatorChallenge.Core.Strategies;
using ElevatorChallenge.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace ElevatorChallenge.ConsoleApp;

class Program
{
  public static async Task Main(string[] args)
  {
    // Set up .NET 8 host with dependency injection
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(ConfigureServices)
        .Build();

    // Get the ElevatorService from DI
    var elevatorService = host.Services.GetRequiredService<IElevatorService>();

    Console.WriteLine("Welcome to the Elevator Service!");

    // Interactive loop
    while (true)
    {
      Console.WriteLine("Enter the floor to request an elevator (or type 'exit' to quit):");
      string? input = Console.ReadLine();

      if (input?.ToLower() == "exit")
        break;

      if (int.TryParse(input, out int requestedFloor))
      {
        Console.WriteLine("Enter the number of people waiting:");
        if (!int.TryParse(Console.ReadLine(), out int peopleWaiting))
        {
          Console.WriteLine("Invalid input. Please enter a valid number.");
          continue;
        }

        Console.WriteLine("Enter the number of people to unload at the floor:");
        if (!int.TryParse(Console.ReadLine(), out int peopleToUnload))
        {
          Console.WriteLine("Invalid input. Please enter a valid number.");
          continue;
        }

        // Request elevator asynchronously
        try
        {
          await elevatorService.RequestElevatorAsync(requestedFloor, peopleWaiting, peopleToUnload);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }

        // Display the status of elevators
        elevatorService.DisplayElevatorStatus();
      }
      else
      {
        Console.WriteLine("Invalid floor number. Please try again.");
      }
    }

    Console.WriteLine("Exiting Elevator Service.");
  }

  private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
  {
    // Register services
    services.AddSingleton<List<Elevator>>(new List<Elevator>
   {
                 new Elevator(5, 1), // Elevator with capacity 5
                 new Elevator(8, 2), // Elevator with capacity 8
                 new Elevator(10, 3) // Elevator with capacity 10
   });
    services.AddSingleton<IElevatorSelectionStrategy, LeastBusyElevatorStrategy>();
    services.AddSingleton<IElevatorService, ElevatorService>();
    services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
    //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    RegisterHandlers(services);

  }
  public static void RegisterHandlers(IServiceCollection services)
  {
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(typeof(MoveToFloorEventHandler).Assembly);
      cfg.RegisterServicesFromAssembly(typeof(LoadPeopleEventHandler).Assembly);
      //cfg.RegisterServicesFromAssembly(typeof(UnloadPeopleEventHandler).Assembly);
    });


  }

}
