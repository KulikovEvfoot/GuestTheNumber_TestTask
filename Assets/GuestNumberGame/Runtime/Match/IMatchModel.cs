using Common;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchModel
    {
        IEventProducer<IMatchRestartObserver> MatchRestartEvent { get; }
        IEventProducer<IStepTranslateObserver> StepTranslateEvent { get; }
        IEventProducer<IMatchFinishObserver> MatchFinishEvent { get; }
        
        IMatchReadOnlyData MatchData { get; }

        void Guess(int number);
        void Restart();
    }
}