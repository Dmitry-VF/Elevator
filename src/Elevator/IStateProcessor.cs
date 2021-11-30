namespace Elevator
{
    public interface IStateProcessor
    {
        void Introduction();
        State ProcessInput();
    }
}
