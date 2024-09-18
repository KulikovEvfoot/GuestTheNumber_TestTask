using System;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.View
{
    public class MatchPresenter : IDisposable
    {
        private readonly IMatchModel m_MatchModel;

        private IPlayer m_CurrentPlayer;
        
        public MatchPresenter(IMatchModel matchModel)
        {
            m_MatchModel = matchModel;
            
            //подписки
        }

        private void OnMatchStateChange(MatchState newState)
        {
            if (newState == MatchState.Reset)
            {
                
            }
            
            if (newState == MatchState.PassStep)
            {
                
            }
        }

        private void SubmitSignal(string numberStr)
        {
            if (!int.TryParse(numberStr, out var number))
            {
                //по жопе
                return;
            }

            m_CurrentPlayer.StepData.Choosen = number;
            m_MatchModel.Update();
        }

        private void ResetSignal()
        {
            m_MatchModel.Reset();
        }

        public void Dispose()
        {
            //отписки
        }
    }
    
}