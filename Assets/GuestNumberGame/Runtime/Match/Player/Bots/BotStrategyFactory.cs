using System;
using Range = Common.Range;

namespace GuestNumberGame.Runtime.Match.Player.Bots
{
    public class BotFactory
    {
        private readonly IReadOnlyMatchStats m_MatchStats;
        private readonly Range m_GuessRange;

        public BotFactory(Range guessRange, IReadOnlyMatchStats matchStats)
        {
            m_GuessRange = guessRange;
            m_MatchStats = matchStats;
        }

        public IPlayer Create(BotComplexity botComplexity)
        {
            var bot = new AIBot($"{botComplexity.ToString()} bot");
            
            switch (botComplexity)
            {
                case BotComplexity.Easy:
                    bot.SetGuessStrategy(new EasyBotStrategy(m_GuessRange, m_MatchStats));
                    break;
                case BotComplexity.Normal:
                    bot.SetGuessStrategy(new MediumBotStrategy(m_GuessRange, m_MatchStats));
                    break;
                case BotComplexity.Hard:
                    bot.SetGuessStrategy(new HardBotStrategy(m_GuessRange, m_MatchStats));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(botComplexity), botComplexity, null);
            }
            
            return bot;
        }
    }
}