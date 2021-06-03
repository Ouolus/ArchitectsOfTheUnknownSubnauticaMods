using ArchitectsLibrary.Items;
using ArchitectsLibrary.Handlers;
using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RotA.Prefabs
{
    public class OmegaCube : ReskinCraftable
    {
        public OmegaCube() : base("OmegaCube", "Omega Cube", "Complex alien material with gargantuan energy capacity. Applications in warp drive technology.")
        {
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override bool UnlockedAtStart => false;

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach (var renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                renderer.material.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f));
                renderer.material.SetColor("_SpecColor", new Color(1f, 1f, 1f));
                renderer.material.SetColor("_DetailsColor", new Color(1f, 2f, 1.25f));
                renderer.material.SetColor("_SquaresColor", new Color(0.5f, 0.5f, 0.5f));
            }
            prefab.GetComponentInChildren<Light>().color = new Color(0.8f, 1f, 1f);
        }

        public override float CraftingTime => 30f;

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new(TechType.Unobtanium, 1),
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("OmegaCube_Icon"));
        }

        protected override string ReferenceClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";
    }
}
