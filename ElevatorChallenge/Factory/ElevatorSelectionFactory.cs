using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Factory
{
    public static class ElevatorSelectionFactory
    {
        public static IElevatorSelectionStrategy CreateStrategy(int choice)
        {
            switch (choice)
            {
                case 1:
                    return new NearestElevatorStrategy();
                case 2:
                    return new LeastBusyElevatorStrategy();
                default:
                    throw new ArgumentException("Invalid strategy choice");
            }
        }
    }
}
