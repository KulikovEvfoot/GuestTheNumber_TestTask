using System;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player.Bots;
using NUnit.Framework;
using Range = Common.Range;

namespace GuestNumberGame.Tests
{
    public class PredictorTest
    {
        private NumberPredictor m_NumberPredictor;

        [SetUp]
        public void Setup()
        {
            var range = new Range(int.MinValue, int.MaxValue);
            
            m_NumberPredictor = new NumberPredictor(range);
        }

        [Test]
        public void Should_Guess()
        {
            var toGuess = int.MinValue;
            var iterationCount = (int) Math.Round(Math.Log((double)int.MaxValue * 2 + 1, 2)); // 32
            var str = string.Empty;
            var predict = m_NumberPredictor.Predict();
            for (int i = 0; i < iterationCount; i++)
            {
                predict = m_NumberPredictor.Predict(new Guess(str, i, predict, GetGuessResult(predict)));
            }

            Assert.AreEqual(toGuess, predict);

            GuessResult GetGuessResult(int number)
            {
                if (number > toGuess)
                {
                    return GuessResult.NeedLess;
                }

                if (number < toGuess)
                {
                    return GuessResult.NeedMore;
                }

                return GuessResult.Guessed;
            }
        }
    }
}