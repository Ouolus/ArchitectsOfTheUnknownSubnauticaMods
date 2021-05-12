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
        public AlienCompositeGlass() : base("ReinforcedGlass", "Reinforced Glass", "Glass reinforced with sapphire.")
        {
        }

        protected override string ReferenceClassId => "86589e2f-bd06-447f-b23a-1f35e6368010";

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(Handlers.AUHandler.SapphireTechType, 2)
                }
            };
        }
    }
}
