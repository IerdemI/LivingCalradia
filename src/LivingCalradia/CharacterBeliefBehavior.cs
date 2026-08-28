using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace LivingCalradia
{
    public class CharacterBeliefBehavior : CampaignBehaviorBase
    {
        private List<CharacterBelief> _beliefs = new List<CharacterBelief>();

        public override void RegisterEvents()
        {
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("LivingCalradia_Beliefs", ref _beliefs);
        }

        public void AddBelief(CharacterBelief belief)
        {
            _beliefs.Add(belief);
        }

        public IReadOnlyList<CharacterBelief> GetBeliefs()
        {
            return _beliefs;
        }

        public IEnumerable<CharacterBelief> GetBeliefsForHero(string heroId)
        {
            return _beliefs.Where(b => b.HeroId == heroId);
        }

    }



}