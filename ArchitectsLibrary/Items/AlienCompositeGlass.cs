using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Items
{
    class AlienCompositeGlass : ReskinCraftable
    {
        public AlienCompositeGlass() : base("AlienCompositeGlass", "Alien composite glass", "Extremely resistant glass, infused with alien technology.")
        {
        }

        protected override string ReferenceClassId => "7965512f-39fe-4770-9060-98bf149bca2e";

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override bool UnlockedAtStart => false;

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        public override float CraftingTime => 8f;

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new(AUHandler.ReinforcedGlassTechType, 1), new(AUHandler.EmeraldTechType, 1)
                }
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>(true);
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Material_Precursor_Glass_specular"));
            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>(true);
            vfxFabricating.scaleFactor = 1.5f;
            vfxFabricating.localMinY = -0.15f;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("Precursor_glass"));
        }
    }
}
