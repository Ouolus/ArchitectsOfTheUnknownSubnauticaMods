using ArchitectsLibrary.Handlers;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;

namespace ArchitectsLibrary.Buildables
{
    class BuildableRedIonCubePedestal : BuildableIonCubePedestal
    {
        public BuildableRedIonCubePedestal(string classId, string displayName, string desc) : base(classId, displayName, desc)
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1), new Ingredient(AUHandler.RedIonCubeTechType, 1) });
        }

        protected override string GetSpriteName => "RedIonCubePedestal";

        protected override string IonCubeClassId { get { return CraftData.GetClassIdForTechType(AUHandler.RedIonCubeTechType); } }
    }
}
