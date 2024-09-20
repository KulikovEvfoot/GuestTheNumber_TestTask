using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(Button))]
    public class TextButton : MonoBehaviour
    {
        [SerializeField] private GameObject m_ButtonRoot;
        [SerializeField] private GameObject m_TextRoot;
        [SerializeField] private UnityEvent<string> m_ChangeTextEvent;
        
        private Action m_OnClick;

        public void Setup(string text, Action onClick)
        {
            SetText(text);
            SetOnClickCall(onClick);
        }
        
        public virtual void SetActive(bool isActive)
        {
            m_ButtonRoot.SetActive(isActive);
        }
        
        public virtual void SetTextActive(bool isActive)
        {
            m_TextRoot.SetActive(isActive);
        }
        
        public void SetOnClickCall(Action onClick)
        {
            m_OnClick = onClick;
        }
        
        public void SetText(string text)
        {
            m_ChangeTextEvent?.Invoke(text);
        }

        public virtual void OnClick()
        {
            m_OnClick?.Invoke();
        }

        private void OnDestroy()
        {
            m_OnClick = null;
        }
    }
}