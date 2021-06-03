using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Buildables;
using ArchitectsLibrary.Handlers;
using SMLHelper.V2.Crafting;

namespace RotA.Prefabs.Buildable
{
    public class OmegaCubePedestal : BuildableIonCubePedestal
    {
        public OmegaCubePedestal() : base("BuildableOmegaCubePedestal", "Omega Cube Pedestal", "A platform containing an Omega Cube. Placeable inside and outside.")
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1), new Ingredient(Mod.omegaCube.TechType, 1) });
        }

        protected override string IonCubeClassId => Mod.omegaCube.ClassID;
    }
}
