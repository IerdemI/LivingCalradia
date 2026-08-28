using TaleWorlds.SaveSystem;

namespace LivingCalradia
{
    public class CharacterBelief
    {
        [SaveableProperty(1)]
        public string HeroId { get; set; }

        [SaveableProperty(2)]
        public string Claim { get; set; }

        [SaveableProperty(3)]
        public int Confidence { get; set; }

        [SaveableProperty(4)]
        public string Source { get; set; }
    }
}