using System;
using Range = Common.Range;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class NumberPredictor
    {
        public Range GuessRange { get; private set; }

        public NumberPredictor(Range guessRange)
        {
            GuessRange = guessRange;
        }

        public int Predict()
        {
            return (GuessRange.Min + GuessRange.Max) / 2;
        }
        
        public int Predict(Guess guess)
        {
            var newMin = GuessRange.Min;
            var newMax = GuessRange.Max;
            
            if (guess.GuessResult == GuessResult.NeedLess)
            {
                newMax = Math.Min(newMax, guess.Number);
            }

            if (guess.GuessResult == GuessResult.NeedMore)
            {
                newMin = Math.Max(newMin, guess.Number);
            }
            
            GuessRange = new Range(newMin, newMax);
            return (newMin + newMax) / 2;
        }
    }
}