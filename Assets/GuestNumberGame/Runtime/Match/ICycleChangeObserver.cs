namespace GuestNumberGame.Runtime.Match
{
    public interface ICycleChangeObserver
    {
        void NotifyOnCycleChanged(int newCycle);
    }
}