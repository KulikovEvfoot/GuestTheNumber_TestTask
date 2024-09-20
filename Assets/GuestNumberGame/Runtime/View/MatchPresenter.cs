using System;
using System.Linq;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player;
using GuestNumberGame.Runtime.Match.Player.Users;

namespace GuestNumberGame.Runtime.View
{
    public class MatchPresenter : 
        IMatchRestartObserver,
        IMatchFinishObserver,
        IMatchNewGuessObserver,
        IGuessObserver,
        IDisposable
    {
        private readonly IMatchModel m_MatchModel;
        private readonly MainMatchView m_MainMatchView;
        
        public MatchPresenter(IMatchModel matchModel, MainMatchView mainMatchView, UserInput userInput)
        {
            m_MatchModel = matchModel;
            m_MainMatchView = mainMatchView;

            var playersData = matchModel.MatchData.PlayersData.ToList();
            if (playersData.Count != 2)
            {
                throw new Exception("Current view work only for 2 players :(");
            }
            
            m_MainMatchView.Init(userInput.Submit);
            
            m_MatchModel.MatchRestartEvent.Attach(this);
            m_MatchModel.MatchFinishEvent.Attach(this);
            m_MatchModel.MatchData.MatchStatsChangeObserver.Attach(this);
            
            foreach (var playerData in m_MatchModel.MatchData.PlayersData)
            {
                playerData.GuessEvent.Attach(this);
            }
        }
        
        public void NotifyOnNewGuess(Guess guess)
        {
            m_MainMatchView.AddGuess(guess);
            m_MainMatchView.UpdateInputField();
        }
        
        public void NotifyOnMatchRestartChange()
        {
            m_MainMatchView.SetActive(true);
            m_MainMatchView.Clear();
        }

        public void NotifyOnMatchFinished(IReadOnlyPlayerData playerData, int answer)
        {
            m_MainMatchView.SetActive(false);
        }
        
        void IGuessObserver.NotifyOnGuess(int? number)
        {
            if (number.HasValue)
            {
                m_MatchModel.Guess(number.Value);
                return;
            }

            m_MainMatchView.SetInputFieldError();
        }

        private void ResetSignal()
        {
            m_MatchModel.Restart();
        }

        public void Dispose()
        {
            m_MatchModel.MatchRestartEvent.Attach(this);
            m_MatchModel.MatchFinishEvent.Attach(this);
            m_MatchModel.MatchData.MatchStatsChangeObserver.Detach(this);

            foreach (var playerData in m_MatchModel.MatchData.PlayersData)
            {
                playerData.GuessEvent.Detach(this);
            }
        }
    }
}