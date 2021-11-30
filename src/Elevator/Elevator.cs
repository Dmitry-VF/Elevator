using System.Collections.Generic;

namespace Elevator
{
    public class Elevator
    {
        private State _state = State.Intro;

        private Dictionary<State, IStateProcessor> _stateProcessors = new Dictionary<State, IStateProcessor>();

        public Elevator()
        {
            _stateProcessors.Add(State.Intro, new Intro());
            _stateProcessors.Add(State.CurrentFloor, new CurrentFloor());
            _stateProcessors.Add(State.Interaction, new Interaction());
        }

        public static int FloorsCount { get; set; }
        public static int CurrentFloor { get; set; }

        public void Use()
        {
            while (_state != State.End)
            {
                var processor = _stateProcessors[_state];
                processor.Introduction();
                _state = processor.ProcessInput();
            }
        }
    }
}

