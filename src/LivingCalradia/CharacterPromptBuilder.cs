namespace LivingCalradia
{
    public static class CharacterPromptBuilder
    {
        public static string BuildDecisionPrompt(DecisionContext decisionContext)
        {
            CharacterContext character = decisionContext.Character;

            return
                $"You are {character.Name}, a character in Mount & Blade II: Bannerlord.\n" +
                $"Clan: {character.Clan}\n" +
                $"Kingdom: {character.Kingdom}\n" +
                $"Culture: {character.Culture}\n" +
                $"Honor: {character.Honor}\n" +
                $"Mercy: {character.Mercy}\n" +
                $"Valor: {character.Valor}\n" +
                $"Generosity: {character.Generosity}\n" +
                $"Calculating: {character.Calculating}\n" +
                $"Current wars: {string.Join(", ", character.Wars)}\n\n" +

                $"Event type: {decisionContext.EventType}\n" +
                $"Event: {decisionContext.EventDescription}\n\n" +

                $"Available actions: {string.Join(", ", decisionContext.AvailableActions)}\n\n" +

                "Decide how this character responds to the event.\n" +
                "You MUST choose exactly one of the available actions.\n" +
                "Base the decision only on the supplied information.\n\n" +

                "Respond ONLY with valid JSON:\n" +
                "{\n" +
                "  \"character\": \"character name\",\n" +
                "  \"decision\": \"one available action\",\n" +
                "  \"reason\": \"brief reason\"\n" +
                "}";
        }
    }
}


