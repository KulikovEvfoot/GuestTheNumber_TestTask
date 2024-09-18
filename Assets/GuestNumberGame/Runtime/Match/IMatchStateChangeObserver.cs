namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchStateChangeObserver
    {
        void NotifyOnMatchStateChange(MatchState newState);
    }
}