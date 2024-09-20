using System;

namespace GuestNumberGame.Runtime.Match.Player.Users
{
    public class User : BasePlayer, IDisposable
    {
        private readonly UserInput m_UserInput;

        public User(string name, UserInput userInput) : base(name)
        {
            m_UserInput = userInput;
            userInput.OnInputSubmit += OnInputSubmit;
        }

        private void OnInputSubmit(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                m_GuessEventProducer.NotifyAll(obs => obs.NotifyOnGuess(null));
                return;
            }

            if (int.TryParse(number, out var result))
            {
                m_GuessEventProducer.NotifyAll(obs => obs.NotifyOnGuess(result));
                return;
            }
            
            m_GuessEventProducer.NotifyAll(obs => obs.NotifyOnGuess(null));
        }
        
        public void Dispose()
        {
            m_UserInput.OnInputSubmit -= OnInputSubmit;
        }
    }
}