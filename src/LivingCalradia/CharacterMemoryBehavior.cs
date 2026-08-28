using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace LivingCalradia
{
    public class CharacterMemoryBehavior : CampaignBehaviorBase
    {
        private List<CharacterMemory> _memories = new List<CharacterMemory>();

        public override void RegisterEvents()
        {
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("LivingCalradia_Memories", ref _memories);
        }

        public void AddMemory(CharacterMemory memory)
        {
            _memories.Add(memory);
        }

        public IReadOnlyList<CharacterMemory> GetMemories()
        {
            return _memories;
        }
        public bool HasMemory(string heroId, string description)
        {
            return _memories.Exists(m =>
                m.HeroId == heroId &&
                m.Description == description);
        }


    }
}