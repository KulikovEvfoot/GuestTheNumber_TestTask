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
            return InternalGuess(GuessRange);
        }
        
        public int Predict(Guess guess)
        {
            var newMin = GuessRange.Min;
            var newMax = GuessRange.Max;
            
            if (guess.GuessResult == GuessResult.NeedLess)
            {
                if (guess.Number < newMax)
                {
                    newMax = guess.Number;
                }
                
                if (newMax == guess.Number)
                {
                    newMax--;
                }
            }

            if (guess.GuessResult == GuessResult.NeedMore)
            {
                if (guess.Number > newMin)
                {
                    newMin = guess.Number;
                }
                
                newMin = Math.Max(newMin, guess.Number);
                if (newMin == guess.Number)
                {
                    newMin++;
                }
            }
            
            GuessRange = new Range(newMin, newMax);
            return InternalGuess(GuessRange);
        }

        private int InternalGuess(Range range)
        {
            return (int)Math.Round(((double)range.Min + (double)range.Max) / 2);
        }
    }
}