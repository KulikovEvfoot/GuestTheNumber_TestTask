using Common;
using GuestNumberGame.Runtime.Player;

namespace GuestNumberGame.Runtime.Match
{
    public class MatchModel : IMatchModel
    {
        private readonly EventProducer<IMatchStateChangeObserver> m_MatchStateChangeEventProducer = new();
        private readonly EventProducer<IStepTranslatedObserver> m_StepTranslatedEventProducer = new();
        private readonly EventProducer<ICycleChangeObserver> m_CycleChangeEventProducer = new();
        
        private readonly MatchData m_MatchData;
        
        public IEventProducer<IMatchStateChangeObserver> MatchStateChangeEvent => m_MatchStateChangeEventProducer;
        public IEventProducer<IStepTranslatedObserver> StepTranslatedEvent => m_StepTranslatedEventProducer;
        public IEventProducer<ICycleChangeObserver> CycleChangeEvent => m_CycleChangeEventProducer;
        
        public IMatchReadOnlyData MatchData => m_MatchData;

        public MatchModel(params IPlayer[] players)
        {
            m_MatchData = new MatchData();
            m_MatchData.SetPlayers(players);
        }
        
        public void Update()
        {
            var currentPlayer = m_MatchData.CurrentPlayer;
            if (m_MatchData.CurrentPlayer.StepData.Step != Step.Completed)
            {
                return;
            }

            //подумать как сделать ивент
            // лучше по очереди передавать ход, на случай финиша
            // PassStep(currentPlayer);

            if (m_MatchData.IsMatchFinished())
            {
                m_MatchStateChangeEventProducer.NotifyAll(
                    obs => obs.NotifyOnMatchStateChange(MatchState.Finished));
                return;
            }
            
            if (m_MatchData.CurrentPlayerStepIndex == m_MatchData.PlayersCount - 1)
            {
                NextCycle();
            }

            StartStep(m_MatchData.CurrentPlayer);
        }

        public void Reset()
        {
            m_MatchData.Reset();
            m_MatchStateChangeEventProducer.NotifyAll(
                obs => obs.NotifyOnMatchStateChange(MatchState.Reset));
        }

        private void StartStep(IPlayer currentPlayer)
        {
            currentPlayer.TakeLead();
        }

        private void NextCycle()
        {
            m_MatchData.IncCycle();
            m_MatchData.ToNextStep();
            
            m_CycleChangeEventProducer.NotifyAll(
                obs => obs.NotifyOnCycleChanged(m_MatchData.Cycle));
        }

        private void PassStep(IPlayer from, IPlayer to)
        {
            to.TakeDownLead();
            
            m_StepTranslatedEventProducer.NotifyAll(
                obs => obs.NotifyOnStepTranslated(from, to));
        }
    }
}