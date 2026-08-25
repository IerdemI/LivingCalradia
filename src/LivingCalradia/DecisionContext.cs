using System.Collections.Generic;

namespace LivingCalradia
{
    public class DecisionContext
    {
        public string EventType { get; set; }
        public string EventDescription { get; set; }

        public CharacterContext Character { get; set; }

        public List<string> AvailableActions { get; set; }
            = new List<string>();
    }
}