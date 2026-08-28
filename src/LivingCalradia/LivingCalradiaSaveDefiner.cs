using TaleWorlds.SaveSystem;

namespace LivingCalradia
{
    public class LivingCalradiaSaveDefiner : SaveableTypeDefiner
    {
        public LivingCalradiaSaveDefiner()
            : base(2100000)
        {
        }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(CharacterMemory), 1);
            AddClassDefinition(typeof(CharacterBelief), 2);
        }

        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(System.Collections.Generic.List<CharacterMemory>));
            ConstructContainerDefinition(typeof(System.Collections.Generic.List<CharacterBelief>));
        }
    }
}