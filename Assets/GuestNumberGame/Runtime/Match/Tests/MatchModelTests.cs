using NUnit.Framework;

namespace GuestNumberGame.Runtime.Match.Tests
{
    public class MatchModelTest
    {
        private IMatchModel m_MatchModel;

        [SetUp]
        public void SetUp()
        {
            m_MatchModel = new MatchModel();
        
        }
    
        [Test]
        public void Should_1()
        {
        
        }
    
        [Test]
        public void Should_2()
        {

            Assert.IsTrue(true);
        }
    }
    
}