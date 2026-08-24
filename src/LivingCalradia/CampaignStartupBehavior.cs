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


            string prompt = CharacterPromptBuilder.BuildTestPrompt(context);

            LlmResponse aiResponse = await LocalLlmClient.SendPromptAsync(prompt);

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
                @"F:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\LivingCalradia\ai_test.txt",
                $"Speaker: {aiResponse.Speaker}\n" +
                $"Response: {aiResponse.Response}\n" +
                $"Intent: {aiResponse.Intent}"
            );

        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}