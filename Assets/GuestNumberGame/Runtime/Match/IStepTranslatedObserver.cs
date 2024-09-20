using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IStepTranslateObserver
    {
        void NotifyOnTranslateStep(IReadOnlyPlayerData from, IReadOnlyPlayerData to);
    }
}