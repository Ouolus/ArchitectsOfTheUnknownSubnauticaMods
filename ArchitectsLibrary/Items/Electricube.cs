namespace ArchitectsLibrary.Items
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using UnityEngine;
    using API;
    using Handlers;
    
    class Electricube : ReskinSpawnable
    {
        public Electricube() : base("Electricube", "Electricube", "A high capacity energy source with a similar structure to the Ion Cube. Has applications in biomechanical materials and warping technology.")
        {
        }

        protected override string ReferenceClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new(AUHandler.MorganiteTechType, 2)
                }
            };
        }

        public override float CraftingTime => 30f;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach(var renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                renderer.material.SetColor("_Color", new Color(0.5f, 0f, 1f) * 0.7f);
                renderer.material.SetColor("_SpecColor", new Color(0f, 0f, 0.9f));
                renderer.material.SetColor("_DetailsColor", new Color(1f, 0f, 1f));
                renderer.material.SetColor("_SquaresColor", new Color(0.5f, 0f, 0.5f));
                renderer.material.SetFloat("_SquaresTile", 45f);
                renderer.material.SetFloat("_SquaresSpeed", 5f);
            }
            prefab.GetComponentInChildren<Light>().color = new Color(1f, 0f, 1f);
            Main.IonCubeCraftModelFix(prefab);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new (Main.assetBundle.LoadAsset<Sprite>("Electricube_Icon"));
        }
    }
}
