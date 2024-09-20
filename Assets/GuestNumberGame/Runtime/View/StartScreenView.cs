using System;
using System.Collections.Generic;
using GuestNumberGame.Runtime.Match.Player.Bots;
using UnityEngine;
using UnityEngine.UI;

namespace GuestNumberGame.Runtime.View
{
    public class StartScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Root;
        [SerializeField] private InputField m_RangeMin;
        [SerializeField] private InputField m_RangeMax;
        [SerializeField] private Dropdown m_DropDown;
        [SerializeField] private Button m_PlayButton;

        private BotComplexity m_ChosenComplexity;
        private Action<int, int, BotComplexity> m_OnSubmit;

        public void Init(Action<int, int, BotComplexity> onSubmit)
        {
            m_OnSubmit = onSubmit;
            
            m_PlayButton.onClick.AddListener(Submit);
            
            PopulateDropDownWithEnum(m_DropDown, m_ChosenComplexity);
        }
        
        public void SetActive(bool isActive)
        {
            m_Root.SetActive(isActive);
        }
        
        private void Submit()
        {
            if (!int.TryParse(m_RangeMin.text, out var min))
            {
                Debug.LogError("Can't parse min range");
                return;
            }
            
            if (!int.TryParse(m_RangeMax.text, out var max))
            {
                Debug.LogError("Can't parse max range");
                return;
            }

            if (min > max)
            {
                Debug.LogError("Min > Max. You Crazy :))");
                return;
            }

            var index = m_DropDown.value;
            var complexity = (BotComplexity)Enum.Parse(typeof(BotComplexity), m_DropDown.options[index].text);
            
            m_OnSubmit.Invoke(min, max, complexity);
        }
        
        private void PopulateDropDownWithEnum(Dropdown dropdown, Enum targetEnum)
        {
            Type enumType = targetEnum.GetType();
            List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();

            for(int i = 0; i < Enum.GetNames(enumType).Length; i++)
            {
                newOptions.Add(new Dropdown.OptionData(Enum.GetName(enumType, i)));
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(newOptions);
        }
    }
}