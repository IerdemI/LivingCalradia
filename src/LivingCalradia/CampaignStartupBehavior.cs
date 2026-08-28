using System.IO;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace LivingCalradia
{
    public class CampaignStartupBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(
                this,
                OnSessionLaunched
            );
        }

        private async void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
        {
            Hero testHero = Hero.AllAliveHeroes
            .FirstOrDefault(h => h != Hero.MainHero && h.IsLord);

            CharacterContext context = CharacterContextReader.BuildContext(testHero);
            CharacterMemoryBehavior memoryBehavior =
                Campaign.Current.GetCampaignBehavior<CharacterMemoryBehavior>();
            CharacterBeliefBehavior beliefBehavior =
                Campaign.Current.GetCampaignBehavior<CharacterBeliefBehavior>();

            string testDescription =
                "The player promised to support Lucon in a future political dispute.";

            if (!memoryBehavior.HasMemory(testHero.StringId, testDescription))
            {
                memoryBehavior.AddMemory(new CharacterMemory
                {
                    HeroId = testHero.StringId,
                    Description = testDescription,
                    Importance = 90
                });
            }

            string testClaim =
                 "The Khuzait war is strategically necessary.";

            if (!beliefBehavior.GetBeliefs().Any(b =>
                b.HeroId == testHero.StringId &&
                b.Claim == testClaim))
            {
                beliefBehavior.AddBelief(new CharacterBelief
                {
                    HeroId = testHero.StringId,
                    Claim = testClaim,
                    Confidence = 75,
                    Source = "Test"
                });
            }



            Hero secondHero = Hero.AllAliveHeroes
                .FirstOrDefault(h =>
                    h != Hero.MainHero &&
                    h.IsLord &&
                    h != testHero);

            string secondClaim =
               "The Khuzait war is strategically unnecessary.";

            if (!beliefBehavior.GetBeliefs().Any(b =>
                b.HeroId == secondHero.StringId &&
                b.Claim == secondClaim))
            {
                beliefBehavior.AddBelief(new CharacterBelief
                {
                    HeroId = secondHero.StringId,
                    Claim = secondClaim,
                    Confidence = 70,
                    Source = "Test"
                });
            }

            File.WriteAllText(
                 @"F:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\LivingCalradia\memory_test.txt",
                 string.Join(
                    "\n\n",
                    memoryBehavior.GetMemories().Select(m =>
                        $"HeroId: {m.HeroId}\n" +
                        $"Description: {m.Description}\n" +
                        $"Importance: {m.Importance}"
                    )
                )
            );

            DecisionContext decisionContext = new DecisionContext
            {
                EventType = "WAR_STATUS",
                EventDescription = "Your kingdom is currently at war with the Khuzait.",
                Character = context,
                AvailableActions =
                {
                    "SUPPORT_WAR",
                    "SEEK_PEACE",
                    "NEUTRAL"
                }
            };

            string decisionPrompt =
                 CharacterPromptBuilder.BuildDecisionPrompt(decisionContext);

            AiDecisionResponse decision =
                await LocalLlmClient.SendDecisionAsync(decisionPrompt);

            File.WriteAllText(
                @"F:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\LivingCalradia\decision_test.txt",
                $"Character: {decision.Character}\n" +
                $"Decision: {decision.Decision}\n" +
                $"Reason: {decision.Reason}"
            );

            File.WriteAllText(
                @"F:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\LivingCalradia\context_test.txt",
                $"Name: {context.Name}\n" +
                $"Clan: {context.Clan}\n" +
                $"Kingdom: {context.Kingdom}\n" +
                $"Fiefs: {string.Join(", ", context.Fiefs)}\n" +
                $"Wars: {string.Join(", ", context.Wars)}\n" +
                $"Culture: {context.Culture}\n" +
                $"Party Size: {context.PartySize}\n" +
                $"Children: {string.Join(", ", context.Children)}\n" +
                $"Siblings: {string.Join(", ", context.Siblings)}\n" +
                $"Honor: {context.Honor}\n"
            );


            File.WriteAllText(
                @"F:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\LivingCalradia\belief_test.txt",
                string.Join(
                    "\n\n",
        beliefBehavior.GetBeliefsForHero(testHero.StringId).Select(b =>
            $"HeroId: {b.HeroId}\n" +
            $"Claim: {b.Claim}\n" +
            $"Confidence: {b.Confidence}\n" +
            $"Source: {b.Source}"
        )
    )
);

        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}