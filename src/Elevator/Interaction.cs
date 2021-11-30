using System;

namespace Elevator
{
    public class Interaction : StateProcessor
    {
        public override void Introduction()
        {
            Console.WriteLine("Which floor do you want to go to?");
            Console.WriteLine("Press X to quit");
        }

        public override State ProcessInput()
        {
            var input = Console.ReadLine();

            if (input == "X")
            {
                Console.WriteLine("Have a good day!");
                return State.End;
            }

            if (int.TryParse(input, out var result) && result > 0 && result <= Elevator.FloorsCount && result != Elevator.CurrentFloor)
            {
                Console.WriteLine(" [>>][<<]");
                if (Elevator.CurrentFloor < result)
                {
                    for (var i = Elevator.CurrentFloor; i <= result; i++)
                    {
                        Console.WriteLine($" [{i}]");
                        Elevator.CurrentFloor = i;
                    }
                }
                if (Elevator.CurrentFloor > result)
                {
                    for (var i = Elevator.CurrentFloor; i >= result; i--)
                    {
                        Console.WriteLine($" [{i}]");
                        Elevator.CurrentFloor = i;
                    }
                }
                Console.WriteLine(" [<<] [>>]");
                return State.Interaction;
            }
            if (result == Elevator.CurrentFloor)
            {
                Console.WriteLine("You are already on this floor");
                return State.Interaction;
            }
            else
            {
                Console.WriteLine("There is no floor with such a number");
            }

            return State.Interaction;
        }
    }
}