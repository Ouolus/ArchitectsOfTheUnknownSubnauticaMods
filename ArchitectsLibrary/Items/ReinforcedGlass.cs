using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Items
{
    class ReinforcedGlass : ReskinCraftable
    {
        public ReinforcedGlass() : base("ReinforcedGlass", "Reinforced glass", "Strong, highly scratch resistant glass synthesized from sapphire crystal.")
        {
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "AdvancedMaterials" };

        protected override string ReferenceClassId => "7965512f-39fe-4770-9060-98bf149bca2e";

        public override bool UnlockedAtStart => false;
        public override TechType RequiredForUnlock => AUHandler.SapphireTechType;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AUHandler.SapphireTechType, 2)
                }
            };
        }
    }
}
