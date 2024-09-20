using Common;

namespace GuestNumberGame.Runtime.Match.Player
{
    public interface IReadOnlyPlayerData
    {
        string Name { get; }
        bool IsHisStep { get; }
        IEventProducer<IGuessObserver> GuessEvent { get; }
    }
}