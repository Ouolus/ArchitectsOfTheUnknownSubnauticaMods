using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Items
{
    class CyclopsModuleTest : CyclopsUpgrade, ICyclopsOnEquip
    {
        public CyclopsModuleTest() : base("CyclopsModuleTest", LanguageSystem.Default, LanguageSystem.Default)
        {
        }

        public void OnEquip(string slotID, bool equipped, SubRoot sub)
        {
            ErrorMessage.AddMessage("Test module equipped: " + equipped);
            ErrorMessage.AddMessage("Sub is null: " + (sub == null));
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.Titanium, 2)});
        }
    }
}
