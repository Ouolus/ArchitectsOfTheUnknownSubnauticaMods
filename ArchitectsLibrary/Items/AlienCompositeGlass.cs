using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Items
{
    class AlienCompositeGlass : ReskinCraftable
    {
        public AlienCompositeGlass() : base("AlienCompositeGlass", "Alien Composite Glass", "Strong, highly scratch resistant glass synthesized from sapphire and emerald crystals.")
        {
        }

        protected override string ReferenceClassId => "7965512f-39fe-4770-9060-98bf149bca2e";

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(Handlers.AUHandler.SapphireTechType, 2), new Ingredient(Handlers.AUHandler.EmeraldTechType, 1)
                }
            };
        }
    }
}
