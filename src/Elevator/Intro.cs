using System;

namespace Elevator
{
    public class Intro : StateProcessor
    {
        public override void Introduction()
        {
            Console.WriteLine("Hello!");
            Console.WriteLine("Enter the number of floors");
        }

        public override State ProcessInput()
        {
            var input = Console.ReadLine();

            if (input == "X")
            {
                Console.WriteLine("Have a good day!");
                return State.End;
            }

            if (int.TryParse(input, out var result))
            {
                Elevator.FloorsCount = result;
                return State.CurrentFloor;
            }

            return State.Intro;
        }
    }
}