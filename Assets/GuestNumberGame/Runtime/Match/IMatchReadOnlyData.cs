using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchReadOnlyData
    {
        IPlayer CurrentPlayer { get; } 
        int CurrentPlayerStepIndex { get; } 
        int PlayersCount { get; }
        int Cycle { get; } 
        IPlayer[] Players{ get; } 
    }
}