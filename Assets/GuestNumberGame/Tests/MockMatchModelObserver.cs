using System;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Tests
{
    public class MockMatchModelObserver : IMatchRestartObserver, IMatchFinishObserver
    {
        private readonly Action m_Restart;
        private readonly Action m_Finish;

        public MockMatchModelObserver(Action restart, Action finish)
        {
            m_Restart = restart;
            m_Finish = finish;
        }

        public void NotifyOnMatchRestartChange()
        {
            m_Restart?.Invoke();
        }

        public void NotifyOnMatchFinished(IReadOnlyPlayerData playerData, int answer)
        {
            m_Finish?.Invoke();
        }
    }
}