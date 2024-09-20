using System;
using UnityEngine;
using Range = Common.Range;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class HardBotStrategy : IBotGuessStrategy, IMatchNewGuessObserver, IDisposable
    {
        private readonly IReadOnlyMatchStats m_ReadOnlyMatchStats;

        private readonly NumberPredictor m_NumberPredictor;
        private int m_NextPredict = 0;
        
        public HardBotStrategy(Range guessRange, IReadOnlyMatchStats readOnlyMatchStats)
        {
            m_ReadOnlyMatchStats = readOnlyMatchStats;
            m_NumberPredictor = new NumberPredictor(guessRange);
            m_NumberPredictor.Predict();
            
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