using Common;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player.Bots;
using GuestNumberGame.Runtime.Match.Player.Users;
using GuestNumberGame.Runtime.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GuestNumberGame
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private StartScreenView m_StartScreenView;
        [SerializeField] private MainMatchView m_MainMatchView;
        [SerializeField] private FinishMatchView m_FinishMatchView;

        private MatchModel m_MatchModel;
        
        private void Awake()
        {
            m_StartScreenView.Init(onSubmit: StartGame);
            m_FinishMatchView.Init(Reload);
            
            m_StartScreenView.SetActive(true);
        }

        private void StartGame(int min, int max, BotComplexity botComplexity)
        {
            var range = new Range(min, max);
            var matchStats = new MatchStats();
            
            var userInput = new UserInput();
            var player = new User("You", userInput);

            var botsFactory = new BotFactory(range, matchStats);

            var bot = botsFactory.Create(botComplexity);
            
            var matchData = new MatchData(range, matchStats, player, bot);
            m_MatchModel = new MatchModel(matchData);
            var presenter = new MatchPresenter(m_MatchModel, m_MainMatchView, userInput);

            m_StartScreenView.SetActive(false);

            
            m_MatchModel.MatchFinishEvent.Attach(m_FinishMatchView);
            m_MatchModel.Restart();
        }

        private void Reload()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}