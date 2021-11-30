using System;

namespace Elevator
{
    public class CurrentFloor : StateProcessor
    {
        public override void Introduction()
        {
            Console.WriteLine("Enter the current floor");
        }

        public override State ProcessInput()
        {
            var input = Console.ReadLine();

            if (input == "X")
            {
                Console.WriteLine("Have a good day!");
                return State.End;
            }

            if (int.TryParse(input, out var result) && result > 0 && result <= Elevator.FloorsCount)
            {
                Elevator.CurrentFloor = result;
                Console.WriteLine(" [<<] [>>]");
                return State.Interaction;
            }

            return State.CurrentFloor;
        }
    }
}