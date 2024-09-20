using System;
using GuestNumberGame.Runtime.Match;
using GuestNumberGame.Runtime.Match.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GuestNumberGame.Runtime.View
{
    public class FinishMatchView : MonoBehaviour, IMatchFinishObserver
    {
        [SerializeField] private GameObject m_Root;
        [SerializeField] private Text m_FinishText;
        [SerializeField] private Button m_Menu;

        private Action m_ToMenu;
        
        public void Init(Action toMenu)
        {
            m_ToMenu = toMenu;
            m_Menu.onClick.RemoveListener(ToMenu);
            m_Menu.onClick.AddListener(ToMenu);
        }
        
        public void SetResult(string winnerName, int resultGuess)
        {
            m_FinishText.text = $"Winner: {winnerName}. Answer: {resultGuess.ToString()}";
        }
        
        public void SetActive(bool isActive)
        {
            m_Root.SetActive(isActive);
        }

        private void ToMenu()
        {
            m_ToMenu?.Invoke();
        }

        public void NotifyOnMatchFinished(IReadOnlyPlayerData playerData, int answer)
        {
            SetResult(playerData.Name, answer);
            SetActive(true);
        }
    }
}