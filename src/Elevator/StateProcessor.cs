using System;

namespace Elevator
{
    public abstract class StateProcessor : IStateProcessor
    {
        public abstract void Introduction();

        public abstract State ProcessInput();

        protected virtual void ShowWarningMessage()
        {
            Console.WriteLine("There is no floor with such a number");
        }
    }
}
