using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchData : IMatchReadOnlyData
    {
        IPlayer CurrentPlayer { get; } 
        IPlayer[] Players { get; } 
        MatchStats MatchStats { get; }
        
        void ToNextStep();
        void Restart();
    }
}