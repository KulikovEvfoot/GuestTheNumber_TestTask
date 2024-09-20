namespace GuestNumberGame.Runtime.Match.Player
{
    public interface IGuessObserver
    {
        void NotifyOnGuess(int? number);
    }
}