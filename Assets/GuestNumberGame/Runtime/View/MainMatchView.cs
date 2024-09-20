using System;
using System.Collections.Generic;
using GuestNumberGame.Runtime.Match;
using UnityEngine;

namespace GuestNumberGame.Runtime.View
{
    public class MainMatchView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Root;
        [SerializeField] private UserInputField m_UserInputField;
        [SerializeField] private Transform m_GuessContent;
        [SerializeField] private GuessView m_GuessViewTemplate;
        
        public void Init(Action<string> userInputSignal)
        {
            m_UserInputField.Init(userInputSignal);
        }

        public void SetActive(bool isActive)
        {
            m_Root.SetActive(isActive);
        }

        public void UpdateInputField()
        {
            m_UserInputField.Shuffle();
            m_UserInputField.Clear();
        }
        
        public void Clear()
        {
            m_UserInputField.Clear();
            for (int i = m_GuessContent.childCount - 1; i >= 0; i--)
            {
                Destroy(m_GuessContent.GetChild(i).gameObject);
            }
        }

        public void AddGuess(Guess guess)
        {
            var guessView = Instantiate(m_GuessViewTemplate, m_GuessContent);
            guessView.Init(guess.Number.ToString(), guess.GuessResult.ToString());
        }

        public void SetInputFieldError()
        {
            m_UserInputField.SetError();
        }
    }
}