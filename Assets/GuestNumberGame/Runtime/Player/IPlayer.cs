using System;

namespace GuestNumberGame.Runtime.Player
{
    public class StepData 
    {
        public Step Step { get; set; }
        public int? Choosen { get; set; }
    }

    public enum Step
    {
        Waiting = 0,
        Now = 1,
        Completed = 2
    }

    public class ChooseData
    {
        public int Number { get; }

        public ChooseData(int number)
        {
            Number = number;
        }
    }
    
    public interface IPlayer
    {
        StepData StepData { get; }

        void TakeLead();
        void TakeDownLead();
    }

    public interface ICanChooseNumber
    {
        int Choose();
    }

    public class AIBot : IPlayer, ICanChooseNumber
    {
        event Action<ChooseData> OnChoose;

        private StepData m_StepData = new StepData();
        
        public StepData StepData => m_StepData;

        public int Choose()
        {
            return 1;
        }

        public void MakeChoice()
        {
            
        }
        
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