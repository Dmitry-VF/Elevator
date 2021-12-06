using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public class Interaction : StateProcessor
    {
        public override void Introduction()
        {
            Console.WriteLine("Enter the required floor");
            Console.WriteLine("Press X to quit");
        }

        private List<int> Stops { get; set; } = new();
        private List<int> VisitedStops { get; set; } = new();

        public override State ProcessInput()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (string.Equals(input, "X", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Have a good day!");
                    return State.End;
                }

                if (int.TryParse(input, out var result) && result > 0 && result <= Elevator.FloorsCount)
                {
                    Stops.Add(result);
                    break;
                }
                else
                {
                    ShowWarningMessage();
                }
            }

            return StartMove();
        }

        private bool ParseInputWhileExecuting()
        {
            var res = true;
            Task.Run(() =>
            {
                if (Console.KeyAvailable)
                {
                    var parsed = int.TryParse(Console.ReadLine(), out var result);
                    if (result > 0 && result <= Elevator.FloorsCount)
                    {
                        Stops.Add(result);
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                };
            }).Wait();

            return res;
        }

        private State StartMove()
        {
            Console.WriteLine(" [>>][<<]");

            var nextStop = FindNextStop();

            while (nextStop != 0)
            {
                MoveToClosestStop(nextStop);
                nextStop = FindNextStop();
            }

            Stops.Clear();
            VisitedStops.Clear();

            Console.WriteLine(" [<<] [>>]");
            return State.Interaction;
        }

        private void MoveToClosestStop(int stop)
        {
            if (Elevator.CurrentFloor < stop)
            {
                RunInGivenDirection(stop, true);
                return;
            }

            if (Elevator.CurrentFloor > stop)
            {
                RunInGivenDirection(stop, false);
                return;
            }
        }

        private void RunInGivenDirection(int stop, bool direction)
        {
            // if direction is true, it will move up, otherwise down
            var number = direction ? 1 : -1;

            for (int i = Elevator.CurrentFloor; direction ? i <= stop : i >= stop; i += number)
            {
                if (!ParseInputWhileExecuting())
                {
                    ShowWarningMessage();
                }

                Thread.Sleep(10);

                Console.WriteLine($" [{i}]");

                var stopsLeft = Stops.Except(VisitedStops).ToArray();

                if (Stops.Contains(i) && !VisitedStops.Contains(i) && stopsLeft.Length != 1)
                {
                    Console.WriteLine(" [<<] [>>]");
                    Console.WriteLine(" [>>][<<]");
                }
                if (Stops.Contains(i))
                {
                    VisitedStops.Add(i);
                }

                Elevator.CurrentFloor = i;

                
            }
        }

        private int FindNextStop()
        {
            var stopsListExceptVisited = Stops.Except(VisitedStops);
            var closestStop = stopsListExceptVisited.OrderBy(stop => Math.Abs(Elevator.CurrentFloor - stop)).FirstOrDefault();
            return closestStop;
        }
    }
}