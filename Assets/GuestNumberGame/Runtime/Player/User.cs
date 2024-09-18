using System;

namespace GuestNumberGame.Runtime.Player
{
    public class User : IPlayer
    {
        event Action<ChooseData> OnChoose;

        private StepData m_StepData = new StepData();
        
        public StepData StepData => m_StepData;
        
        public void TakeLead()
        {
            m_StepData.Step = Step.Now;
        }

        public void TakeDownLead()
        {
            m_StepData.Step = Step.Waiting;
        }
    }
}