namespace GuestNumberGame.Runtime.Match.Player
{
    public interface IPlayer : IReadOnlyPlayerData
    {
        void SetStepStatus(bool isHisStep);
        void Reset();
    }
}