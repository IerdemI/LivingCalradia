using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace LivingCalradia
{
    public static class CharacterContextReader
    {
        public static string GetBasicContext(Hero hero)
        {
            if (hero == null)
                return "No hero provided.";

            return
                    $"Name: {hero.Name}\n" +
                    $"Clan: {hero.Clan?.Name}\n" +
                    $"Clan Tier: {hero.Clan?.Tier}\n" +
                    $"Clan Leader: {hero.Clan?.Leader == hero}\n" +
                    $"Kingdom Ruler: {hero.Clan?.Kingdom?.Leader == hero}\n" +
                    $"Gold: {hero.Gold}\n" +
                    $"Wars: {GetWars(hero)}\n" +
                    $"Kingdom Strength: {hero.Clan?.Kingdom?.CurrentTotalStrength}\n" +
                    $"Kingdom Clans: {hero.Clan?.Kingdom?.Clans.Count}\n" +
                    $"Kingdom Settlements: {hero.Clan?.Kingdom?.Settlements.Count}\n" +
                    $"Kingdom Fiefs: {hero.Clan?.Kingdom?.Fiefs.Count}\n" +
                    $"Renown: {hero.Clan?.Renown}\n" +
                    $"Influence: {hero.Clan?.Influence}\n" +
                    $"Clan Wealth: {hero.Clan?.Gold}\n" +
                    $"Party Size: {hero.PartyBelongedTo?.MemberRoster?.TotalManCount}\n" +
                    $"Party Prisoners: {hero.PartyBelongedTo?.PrisonRoster?.TotalManCount}\n" +
                    $"Fiefs: {GetFiefs(hero)}\n" +
                    $"Kingdom: {hero.Clan?.Kingdom?.Name}\n" +
                    $"Culture: {hero.Culture?.Name}\n" +
                    $"Age: {hero.Age:0}\n" +
                    $"Female: {hero.IsFemale}\n" +
                    $"Occupation: {hero.Occupation}\n" +
                    $"Traits: {GetTraits(hero)}\n" +
                    $"Alive: {hero.IsAlive}\n" +
                    $"Prisoner: {hero.IsPrisoner}\n" +
                    $"Settlement: {hero.CurrentSettlement?.Name}\n" +
                    $"Home Settlement: {hero.HomeSettlement?.Name}\n" +
                    $"Is Wanderer: {hero.IsWanderer}\n" +
                    $"Is Notable: {hero.IsNotable}\n" +
                    $"Is Fugitive: {hero.IsFugitive}\n" +
                    $"Party: {hero.PartyBelongedTo?.Name}\n" +
                    $"Spouse: {hero.Spouse?.Name}\n" +
                    $"Father: {hero.Father?.Name}\n" +
                    $"Mother: {hero.Mother?.Name}\n" +
                    $"Children: {string.Join(", ", hero.Children)}\n" +
                    $"Siblings: {string.Join(", ", hero.Siblings)}\n" +
                    $"Relation to Player: {hero.GetRelation(Hero.MainHero)}";

        }

        private static string GetTraits(Hero hero)
        {
            return
                $"Valor: {hero.GetTraitLevel(DefaultTraits.Valor)}, " +
                $"Mercy: {hero.GetTraitLevel(DefaultTraits.Mercy)}, " +
                $"Honor: {hero.GetTraitLevel(DefaultTraits.Honor)}, " +
                $"Generosity: {hero.GetTraitLevel(DefaultTraits.Generosity)}, " +
                $"Calculating: {hero.GetTraitLevel(DefaultTraits.Calculating)}";
        }

        private static string GetFiefs(Hero hero)
        {
            if (hero.Clan == null || hero.Clan.Fiefs.Count == 0)
                return "None";

            return string.Join(", ", hero.Clan.Fiefs);
        }

        private static string GetWars(Hero hero)
        {
            var kingdom = hero.Clan?.Kingdom;

            if (kingdom == null)
                return "None";

            var enemies = kingdom.FactionsAtWarWith
                .Where(f => f.IsKingdomFaction)
                .Select(f => f.Name.ToString());

            return enemies.Any()
                ? string.Join(", ", enemies)
                : "None";
        }

        public static CharacterContext BuildContext(Hero hero)
        {
            if (hero == null)
                return null;

            CharacterContext context = new CharacterContext
            {
                Name = hero.Name?.ToString(),
                Clan = hero.Clan?.Name?.ToString(),
                Kingdom = hero.Clan?.Kingdom?.Name?.ToString(),
                Culture = hero.Culture?.Name?.ToString(),

                Age = hero.Age,
                IsFemale = hero.IsFemale,
                Occupation = hero.Occupation.ToString(),

                Valor = hero.GetTraitLevel(DefaultTraits.Valor),
                Mercy = hero.GetTraitLevel(DefaultTraits.Mercy),
                Honor = hero.GetTraitLevel(DefaultTraits.Honor),
                Generosity = hero.GetTraitLevel(DefaultTraits.Generosity),
                Calculating = hero.GetTraitLevel(DefaultTraits.Calculating),

                Party = hero.PartyBelongedTo?.Name?.ToString(),
                PartySize = hero.PartyBelongedTo?.MemberRoster?.TotalManCount,

                CurrentSettlement = hero.CurrentSettlement?.Name?.ToString(),
                HomeSettlement = hero.HomeSettlement?.Name?.ToString(),

                RelationToPlayer = hero.GetRelation(Hero.MainHero)
            };

            if (hero.Clan != null)
            {
                foreach (var fief in hero.Clan.Fiefs)
                    context.Fiefs.Add(fief.Name.ToString());
            }

            if (hero.Clan?.Kingdom != null)
            {
                foreach (var faction in hero.Clan.Kingdom.FactionsAtWarWith)
                {
                    if (faction.IsKingdomFaction)
                        context.Wars.Add(faction.Name.ToString());
                }
            }

            foreach (var child in hero.Children)
                context.Children.Add(child.Name.ToString());

            foreach (var sibling in hero.Siblings)
                context.Siblings.Add(sibling.Name.ToString());

            return context;
        }




    }
}