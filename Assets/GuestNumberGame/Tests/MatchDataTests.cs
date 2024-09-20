using Common;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player;
using NUnit.Framework;

namespace GuestNumberGame.Tests
{
    public class MatchDataTests
    {
        private const int m_NumberToGuess = 123;
        private readonly Range m_Range = new Range(m_NumberToGuess, m_NumberToGuess);
        
        private IMatchData m_MatchData;
        
        [SetUp]
        public void SetUp()
        {
            var mockPlayer1 = new MockPlayer("1");
            var mockPlayer2 = new MockPlayer("2");
            var mockPlayer3 = new MockPlayer("3");

            m_MatchData = new MatchData(
                guessRange: m_Range,
                new MatchStats(),
                players: new IPlayer[] { mockPlayer1, mockPlayer2, mockPlayer3 });
        }
    
        [Test]
        public void Should_One_Cycle_Complete()
        {
            const int predictCycleCount = 1;
            
            var oldCycle = m_MatchData.Cycle;

            for (int i = 0; i < m_MatchData.PlayersCount; i++)
            {
                m_MatchData.ToNextStep();
            }
            
            var newCycle = m_MatchData.Cycle;

            var presumablyOneCycle = newCycle - oldCycle;
            Assert.AreEqual(presumablyOneCycle, predictCycleCount);
        }
    
        [Test]
        public void Should_Five_Cycle_Complete()
        {
            const int predictCycleCount = 5;

            var oldCycle = m_MatchData.Cycle;

            for (int i = 0; i < m_MatchData.PlayersCount * predictCycleCount; i++)
            {
                m_MatchData.ToNextStep();
            }
            
            var newCycle = m_MatchData.Cycle;

            var presumablyOneCycle = newCycle - oldCycle;
            Assert.AreEqual(presumablyOneCycle, predictCycleCount);
        }
        
        [Test]
        public void Should_Cycle_Not_Complete()
        {
            var oldCycle = m_MatchData.Cycle;

            m_MatchData.ToNextStep();
            m_MatchData.ToNextStep();
            
            var newCycle = m_MatchData.Cycle;

            Assert.AreEqual(newCycle, oldCycle);
        }

        [Test]
        public void Should_Restart()
        {
            for (int i = 0; i < m_MatchData.PlayersCount; i++)
            {
                m_MatchData.ToNextStep();
            }

            m_MatchData.Restart();
            
            Assert.AreEqual(m_MatchData.NumberToGuess, m_NumberToGuess);
            Assert.AreEqual(m_MatchData.CurrentPlayerStepIndex, 0);
            Assert.AreEqual(m_MatchData.Cycle, 0);
        } 
    }
}