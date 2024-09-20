using UnityEngine;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class StupidBotStrategy : IBotGuessStrategy
    {
        public int Guess()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }
    }
}