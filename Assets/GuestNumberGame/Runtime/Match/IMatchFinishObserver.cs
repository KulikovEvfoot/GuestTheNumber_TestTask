using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchFinishObserver
    {
        void NotifyOnMatchFinished(IReadOnlyPlayerData playerData, int answer);
    }
}