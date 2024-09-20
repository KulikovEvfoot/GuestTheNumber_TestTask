using Common;

namespace GuestNumberGame.Runtime.Match.Player
{
    public abstract class BasePlayer : IPlayer
    {
        protected readonly EventProducer<IGuessObserver> m_GuessEventProducer = new();

        public string Name { get; }
        public bool IsHisStep { get; private set; }
        public IEventProducer<IGuessObserver> GuessEvent => m_GuessEventProducer;

        protected BasePlayer(string name)
        {
            Name = name;
        }
        
        public virtual void SetStepStatus(bool isHisStep)
        {
            IsHisStep = isHisStep;
        }

        public virtual void Reset()
        {
        }
    }
}