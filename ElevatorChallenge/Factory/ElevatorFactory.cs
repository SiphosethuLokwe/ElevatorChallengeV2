using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Factory
{
    public static class ElevatorFactory
    {
        public static Elevator CreateElevator(int capacity)
        {
            return new Elevator(capacity);
        }
    }
}
