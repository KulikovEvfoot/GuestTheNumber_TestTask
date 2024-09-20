using UnityEngine;
using UnityEngine.UI;

namespace GuestNumberGame.Runtime.View
{
    public class GuessView : MonoBehaviour
    {
        [SerializeField] private Text m_Guess;
        [SerializeField] private Text m_Result;

        public void Init(string guess, string result)
        {
            m_Guess.text = guess;
            m_Result.text = result;
        }
    }
}