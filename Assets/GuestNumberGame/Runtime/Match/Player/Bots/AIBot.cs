namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class AIBot : BasePlayer
    {
        private IBotGuessStrategy m_BotGuessStrategy = new StupidBotStrategy();

        public AIBot(string name) : base(name)
        {
        }

        public void SetGuessStrategy(IBotGuessStrategy strategy)
        {
            m_BotGuessStrategy = strategy;
        }

        public override void SetStepStatus(bool isHisStep)
        {
            base.SetStepStatus(isHisStep);
            if (isHisStep)
            {
                var guess = m_BotGuessStrategy.Guess();
                m_GuessEventProducer.NotifyAll(obs => obs.NotifyOnGuess(guess));
            }
        }
    }
}