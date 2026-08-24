using System.Collections.Generic;

namespace LivingCalradia
{
    public class CharacterContext
    {
        public string Name { get; set; }
        public string Clan { get; set; }
        public string Kingdom { get; set; }
        public string Culture { get; set; }

        public float Age { get; set; }
        public bool IsFemale { get; set; }
        public string Occupation { get; set; }

        public int Valor { get; set; }
        public int Mercy { get; set; }
        public int Honor { get; set; }
        public int Generosity { get; set; }
        public int Calculating { get; set; }

        public string Party { get; set; }
        public int? PartySize { get; set; }

        public string CurrentSettlement { get; set; }
        public string HomeSettlement { get; set; }

        public int RelationToPlayer { get; set; }

        public List<string> Fiefs { get; set; } = new List<string>();
        public List<string> Wars { get; set; } = new List<string>();
        public List<string> Children { get; set; } = new List<string>();
        public List<string> Siblings { get; set; } = new List<string>();
    }
}