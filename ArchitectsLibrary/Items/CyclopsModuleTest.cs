using ArchitectsLibrary.API;
using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Items
{
    class CyclopsModuleTest : CyclopsUpgrade
    {
        public CyclopsModuleTest() : base("CyclopsModuleTest", LanguageSystem.Default, LanguageSystem.Default)
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.Titanium, 2)});
        }
    }
}
