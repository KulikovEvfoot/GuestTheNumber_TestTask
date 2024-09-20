namespace GuestNumberGame.Runtime.Match
{
    public record Guess
    {
        public readonly string PlayerName;
        public readonly int Cycle;
        public readonly int Number;
        public readonly GuessResult GuessResult;

        public Guess(string playerName, int cycle, int number, GuessResult guessResult)
        {
            PlayerName = playerName;
            Cycle = cycle;
            Number = number;
            GuessResult = guessResult;
        }
    }
}