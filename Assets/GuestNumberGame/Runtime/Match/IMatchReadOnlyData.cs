using System.Collections.Generic;
using Common;
using GuestNumberGame.Runtime.Match.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchReadOnlyData
    {
        int NumberToGuess { get; }
        Range GuessRange { get; }
        int CurrentPlayerStepIndex { get; } 
        int PlayersCount { get; }
        int Cycle { get; } 
        IEnumerable<IReadOnlyPlayerData> PlayersData { get; }
        IEventProducer<IMatchNewGuessObserver> MatchStatsChangeObserver { get; }
    }
}