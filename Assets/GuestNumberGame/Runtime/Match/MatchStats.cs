using System.Collections.Generic;
using Common;

namespace GuestNumberGame.Runtime.Match
{
    public class MatchStats : IReadOnlyMatchStats
    {
        private readonly Stack<Guess> m_Guesses = new();

        private readonly EventProducer<IMatchNewGuessObserver> m_NewGuessEvent = new();

        public IEventProducer<IMatchNewGuessObserver> NewGuessEvent => m_NewGuessEvent;
        
        public void TakeGuess(Guess guess)
        {
            m_Guesses.Push(guess);
            
            m_NewGuessEvent.NotifyAll(
                obs => obs.NotifyOnNewGuess(guess));
        }

        public void Clear()
        {
            m_Guesses.Clear();
        }
    }
}