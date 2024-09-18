using System;
using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.Match
{
    public class MatchData : IMatchData
    {
        private int m_CurrentPlayerStepIndex = 0;
        private int m_Cycle = -1;
        private IPlayer[] m_Players;
        private Func<bool> m_MatchFinishCondition;
        
        public MatchState MatchState;
        
        public IPlayer CurrentPlayer => m_Players[m_CurrentPlayerStepIndex];
        public int CurrentPlayerStepIndex => m_CurrentPlayerStepIndex;
        public int PlayersCount => m_Players.Length;
        public int Cycle => m_Cycle;
        public IPlayer[] Players => m_Players;

        public void IncCycle()
        {
            m_Cycle++;
        }

        public bool IsMatchFinished()
        {
            return m_MatchFinishCondition.Invoke();
        }

        public void ToNextStep()
        {
            m_CurrentPlayerStepIndex++;
            if (m_CurrentPlayerStepIndex >= PlayersCount)
            {
                m_CurrentPlayerStepIndex = 0;
            }
        }
        
        public void SetFinishCondition(Func<bool> condition)
        {
            m_MatchFinishCondition = condition;
        }

        public void SetPlayers(params IPlayer[] players)
        {
            m_Players = players;
        }

        public void Reset()
        {
            m_CurrentPlayerStepIndex = 0;
            m_Cycle = -1;
        }
    }
}