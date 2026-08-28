using TaleWorlds.SaveSystem;

namespace LivingCalradia
{
    public class CharacterMemory
    {
        [SaveableProperty(1)]
        public string HeroId { get; set; }

        [SaveableProperty(2)]
        public string Description { get; set; }

        [SaveableProperty(3)]
        public int Importance { get; set; }
    }
}