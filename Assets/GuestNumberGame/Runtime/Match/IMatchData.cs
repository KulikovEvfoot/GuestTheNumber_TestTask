using System;
using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.Match
{
    public interface IMatchData : IMatchReadOnlyData
    {
        void IncCycle();
        bool IsMatchFinished();
        void ToNextStep();
        void SetFinishCondition(Func<bool> condition);
        void SetPlayers(params IPlayer[] players);
        void Reset();
    }
}