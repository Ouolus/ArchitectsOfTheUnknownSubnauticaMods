using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using ArchitectsLibrary.Handlers;
using UnityEngine;

namespace ProjectAncients.Prefabs.Equipment
{
    public class WarpCannonPrefab : Equipable
    {
        public WarpCannonPrefab() : base("WarpCannon", "Handheld Warping Device", "A warp cannon that makes metious go yes.")
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        public override bool UnlockedAtStart => false;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 2), new Ingredient(TechType.PrecursorIonCrystal, 1), new Ingredient(TechType.EnameledGlass, 1) //enameled glass is substitute for reinforced glass
                }
            };
        }
    }
}
