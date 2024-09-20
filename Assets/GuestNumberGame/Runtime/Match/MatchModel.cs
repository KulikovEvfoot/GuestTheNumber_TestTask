using System;
using Common;

namespace GuestNumberGame.Runtime.Match
{
    public class MatchModel : IMatchModel, IDisposable
    {
        private readonly EventProducer<IMatchRestartObserver> m_MatchRestartEventProducer = new();
        private readonly EventProducer<IStepTranslateObserver> m_StepTranslateEventProducer = new();
        private readonly EventProducer<IMatchFinishObserver> m_MatchFinishEventProducer = new();
        
        private readonly IMatchData m_MatchData;
        
        public IEventProducer<IMatchRestartObserver> MatchRestartEvent => m_MatchRestartEventProducer;
        public IEventProducer<IStepTranslateObserver> StepTranslateEvent => m_StepTranslateEventProducer;
        public IEventProducer<IMatchFinishObserver> MatchFinishEvent => m_MatchFinishEventProducer;

        public IMatchReadOnlyData MatchData => m_MatchData;

        public MatchModel(IMatchData matchData)
        {
            m_MatchData = matchData;
        }
        
        public void Guess(int number)
        {
            var guesser = m_MatchData.CurrentPlayer;
            
            m_MatchData.MatchStats.TakeGuess(
                new Guess(guesser.Name, m_MatchData.Cycle, number, GetGuessResult(number)));

            if (number == m_MatchData.NumberToGuess)
            {
                m_MatchFinishEventProducer.NotifyAll(
                    obs => obs.NotifyOnMatchFinished(guesser, m_MatchData.NumberToGuess));
                return;
            }
            
            guesser.SetStepStatus(false);
            m_MatchData.ToNextStep();
            var nextPlayer = m_MatchData.CurrentPlayer;
            nextPlayer.SetStepStatus(true);
            
            m_StepTranslateEventProducer
                .NotifyAll(obs => obs.NotifyOnTranslateStep(guesser, nextPlayer));
        }

        public void Restart()
        {
            m_MatchData.Restart();
            m_MatchRestartEventProducer.NotifyAll(
                obs => obs.NotifyOnMatchRestartChange());
        }

        private GuessResult GetGuessResult(int guess)
        {
            var numberToGuess = m_MatchData.NumberToGuess;
            if (guess > numberToGuess)
            {
                return GuessResult.NeedLess;
            }

            if (guess < numberToGuess)
            {
                return GuessResult.NeedMore;
            }

            return GuessResult.Guessed;
        }

        public void Dispose()
        {
            m_MatchRestartEventProducer?.Dispose();
            m_StepTranslateEventProducer?.Dispose();
            m_MatchFinishEventProducer?.Dispose();
        }
    }
}