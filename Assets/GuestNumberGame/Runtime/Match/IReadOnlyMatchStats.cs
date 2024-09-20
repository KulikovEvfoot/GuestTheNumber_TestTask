using Common;

namespace GuestNumberGame.Runtime.Match
{
    public interface IReadOnlyMatchStats
    {
        IEventProducer<IMatchNewGuessObserver> NewGuessEvent { get; }
    }
}