using ArchitectsLibrary.Handlers;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;

namespace ArchitectsLibrary.Buildables
{
    class BuildableElectricubePedestal : BuildableIonCubePedestal
    {
        public BuildableElectricubePedestal(string classId, string displayName, string desc) : base(classId, displayName, desc)
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1), new Ingredient(AUHandler.ElectricubeTechType, 1) });
        }

        protected override string GetSpriteName => "ElectricubePedestal";

        protected override string IonCubeClassId { get { return CraftData.GetClassIdForTechType(AUHandler.ElectricubeTechType); } }
    }
}
