using System.Collections.Generic;
using Common;
using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Runtime.Match
{
    public class MatchData : IMatchData
    {
        private readonly IPlayer[] m_Players;
        private readonly MatchStats m_MatchStats;
        
        private int m_NumberToGuess;
        private int m_CurrentPlayerStepIndex;
        private int m_Cycle;

        public Range GuessRange { get; }
        public int NumberToGuess { get; private set; }
        public IPlayer CurrentPlayer => m_Players[m_CurrentPlayerStepIndex];
        public int CurrentPlayerStepIndex => m_CurrentPlayerStepIndex;
        public int PlayersCount => m_Players.Length;
        public int Cycle => m_Cycle;
        public IPlayer[] Players => m_Players;
        public MatchStats MatchStats => m_MatchStats;
        public IEnumerable<IReadOnlyPlayerData> PlayersData => m_Players;
        public IEventProducer<IMatchNewGuessObserver> MatchStatsChangeObserver => m_MatchStats.NewGuessEvent;

        public MatchData(Range guessRange, MatchStats matchStats, params IPlayer[] players)
        {
            GuessRange = guessRange;
            NumberToGuess = GuessRange.GetRandom();
            m_MatchStats = matchStats;
            m_Players = players;
        }

        public void ToNextStep()
        {
            m_CurrentPlayerStepIndex++;
            if (m_CurrentPlayerStepIndex >= PlayersCount)
            {
                m_CurrentPlayerStepIndex = 0;
                m_Cycle++;
            }
        }
        
        public void Restart()
        {
            NumberToGuess = GuessRange.GetRandom();
            m_CurrentPlayerStepIndex = default;
            m_Cycle = default;

            foreach (var player in m_Players)
            {
                player.Reset();
            }
            
            m_MatchStats.Clear();
        }
    }
}