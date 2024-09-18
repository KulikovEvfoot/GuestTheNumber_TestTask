using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IStepTranslatedObserver
    {
        void NotifyOnStepTranslated(IPlayer from, IPlayer to);
    }
}