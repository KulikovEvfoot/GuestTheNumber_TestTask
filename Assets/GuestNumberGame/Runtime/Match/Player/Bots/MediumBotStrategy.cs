using Common;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class MediumBotStrategy : IBotGuessStrategy, IMatchNewGuessObserver
    {
        private readonly IReadOnlyMatchStats m_ReadOnlyMatchStats;

        private readonly AmenableNumberPredictor m_NumberPredictor;
        private int m_NextPredict = 0;
        
        public MediumBotStrategy(Range guessRange, IReadOnlyMatchStats readOnlyMatchStats)
        {
            m_ReadOnlyMatchStats = readOnlyMatchStats;
            var numberPredictor = new NumberPredictor(guessRange);
            m_NumberPredictor = new AmenableNumberPredictor(numberPredictor);

            m_NextPredict = m_NumberPredictor.Predict();
            
            m_ReadOnlyMatchStats.NewGuessEvent.Attach(this);
        }

        public int Guess()
        {
            return m_NextPredict;
        }

        public void NotifyOnNewGuess(Guess guess)
        {
            m_NextPredict = m_NumberPredictor.Predict(guess);
        }

        public void Dispose()
        {
            m_ReadOnlyMatchStats.NewGuessEvent.Detach(this);
        }
    }
}