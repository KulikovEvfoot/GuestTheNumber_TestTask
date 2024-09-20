namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchNewGuessObserver
    {
        void NotifyOnNewGuess(Guess guess);
    }
}