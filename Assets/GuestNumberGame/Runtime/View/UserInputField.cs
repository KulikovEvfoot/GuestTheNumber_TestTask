using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GuestNumberGame.Runtime.View
{
    public class UserInputField : MonoBehaviour
    {
        [SerializeField] private TextButton[] m_InputDigitsViews;
        [SerializeField] private TextButton m_MinusButton;
        [SerializeField] private TextButton m_EnterButton;
        [SerializeField] private TextButton m_RemoveButton;
        
        [SerializeField] private Text m_InputedText;

        private readonly LinkedList<char> m_Input = new();
        private List<int> m_Indexes = new();
        private Action<string> m_OnEnterClick;
        private bool m_IsPositive = true;
        
        public void Init(Action<string> onEnterClick)
        {
            if (m_InputDigitsViews.Length != 10)
            {
                throw new Exception("неправильно сконигурировали инпут, нужно 10 кнопок ввода");
            }

            m_OnEnterClick = onEnterClick;
            
            m_MinusButton.Setup("-", OnMinusClick);
            m_EnterButton.Setup("Enter", OnEnterClick);
            m_RemoveButton.Setup("Remove", OnRemoveClick);
            m_Indexes = Enumerable.Range(0, 10).ToList();
            UpdateButtons();
            Shuffle();
        }

        public void Shuffle()
        {
            FisherYatesShuffle(m_Indexes);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < m_Indexes.Count; i++)
            {
                var newIndex = m_Indexes[i];
                var str = newIndex.ToString();
                var digit = str[0];
                m_InputDigitsViews[i].Setup(str, () =>
                {
                    OnDigitClick(digit);
                });
            }
        }

        public void SetError()
        {
            m_InputedText.color = Color.red;
        }

        public void Clear()
        {
            m_Input.Clear();
            m_InputedText.text = string.Empty;
            m_InputedText.color = Color.black;
        }

        private void FisherYatesShuffle(List<int> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private void OnDigitClick(char i)
        {
            m_InputedText.color = Color.black;
            
            m_Input.AddLast(i);
            m_InputedText.text = new string(m_Input.ToArray());
        }

        private void OnMinusClick()
        {
            if (m_IsPositive)
            {
                m_Input.AddFirst('-');
            }
            else
            {
                if (m_Input.Count == 0)
                {
                    return;
                }
                
                m_Input.RemoveFirst();
            }
            
            m_InputedText.text = new string(m_Input.ToArray());
            m_InputedText.color = Color.black;
            m_IsPositive = !m_IsPositive;
        }

        private void OnEnterClick()
        {
            m_IsPositive = true;
            m_OnEnterClick?.Invoke(m_InputedText.text);
        }

        private void OnRemoveClick()
        {
            if (m_Input.Count == 0)
            {
                return;
            }
            
            m_Input.RemoveLast();
            m_InputedText.text = new string(m_Input.ToArray());
        }
    }
}