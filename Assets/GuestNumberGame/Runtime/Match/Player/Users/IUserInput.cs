using System;

namespace GuestNumberGame.Runtime.Match.Player.Users
{
    public class UserInput
    {
        public event Action<string> OnInputSubmit;
        
        public void Submit(string number)
        {
            OnInputSubmit?.Invoke(number);
        }
    }
}