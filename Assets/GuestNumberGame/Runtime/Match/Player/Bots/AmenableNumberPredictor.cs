using UnityEngine;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class AmenableNumberPredictor
    {
        private const int m_GuessChance = 50;
        
        private readonly NumberPredictor m_NumberPredictor;

        public AmenableNumberPredictor(NumberPredictor numberPredictor)
        {
            m_NumberPredictor = numberPredictor;
        }
        
        public int Predict()
        {
            var predict = m_NumberPredictor.Predict();

            if (CanGuess())
            {
                return predict;
            }
            
            return m_NumberPredictor.GuessRange.GetRandom();
        }
        
        public int Predict(Guess guess)
        {
            var predict = m_NumberPredictor.Predict(guess);

            if (CanGuess())
            {
                return predict;
            }
            
            return m_NumberPredictor.GuessRange.GetRandom();
        }

        private bool CanGuess()
        {
            var rnd = Random.Range(0, 100);
            return rnd > m_GuessChance;
        }
    }
}