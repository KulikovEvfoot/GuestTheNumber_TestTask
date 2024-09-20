using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player;
using NUnit.Framework;
using Range = Common.Range;

namespace GuestNumberGame.Tests
{
    public class MatchModelTest
    {
        private const int m_NumberToGuess = 123;
        private readonly Range m_Range = new Range(m_NumberToGuess, m_NumberToGuess);
        
        private IMatchData m_MatchData;
        private IMatchModel m_MatchModel;

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
            
            m_MatchModel = new MatchModel(m_MatchData);
        }
    
        [Test]
        public void Should_Finished_Event_Send()
        {
            bool isFinished = false;
            var mockMatchModelObserver = new MockMatchModelObserver(restart: null, finish: () => isFinished = true);
            m_MatchModel.MatchFinishEvent.Attach(mockMatchModelObserver);
            
            m_MatchModel.Guess(m_NumberToGuess);
            
            Assert.IsTrue(isFinished);
        }
    
        [Test]
        public void Should_Restart_Event_Send()
        {           
            bool isRestarted = false;
            var mockMatchModelObserver = new MockMatchModelObserver(restart: () => isRestarted = true, finish: null);
            m_MatchModel.MatchRestartEvent.Attach(mockMatchModelObserver);
            
            m_MatchModel.Restart();

            Assert.IsTrue(isRestarted);
        }
        
        [Test]
        public void Should_Match_Data_Change()
        {
            var matchData = m_MatchModel.MatchData;
            m_MatchModel.Guess(0);
            m_MatchModel.Guess(10);
            m_MatchModel.Guess(6);

            var cycle = matchData.Cycle;
            
            m_MatchModel.Restart();

            Assert.AreNotEqual(cycle, matchData.Cycle);
        }
    }
    
}