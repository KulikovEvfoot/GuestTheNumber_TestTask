using Common;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchModel
    {
        IEventProducer<IMatchStateChangeObserver> MatchStateChangeEvent { get; }
        IEventProducer<IStepTranslatedObserver> StepTranslatedEvent { get; }
        IEventProducer<ICycleChangeObserver> CycleChangeEvent { get; }
        
        IMatchReadOnlyData MatchData { get; }
        
        void Update();
        void Reset();
    }
}