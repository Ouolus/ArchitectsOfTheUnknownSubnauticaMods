namespace ArchitectsLibrary.Items
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using UnityEngine;
    using Handlers;
    using API;
    
    class RedIonCube : ReskinSpawnable
    {
        public RedIonCube() : base("RedIonCube", "Power cube", "A high capacity energy source with a similar structure to the Ion Cube. Capable of releasing massive amounts of energy in a short burst. Applications in powerful offensive and defensive technology.")
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
                    new(AUHandler.RedBerylTechType, 2)
                }
            };
        }

        public override float CraftingTime => 30f;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach(var renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                renderer.material.SetColor("_Color", new Color(1f, 0.2f, 0f));
                renderer.material.SetColor("_SpecColor", new Color(1f, 0.2f, 0f));
                renderer.material.SetColor("_DetailsColor", new Color(1f, 1f, 1f));
                renderer.material.SetColor("_SquaresColor", new Color(0.5f, 0.5f, 0.5f));
                renderer.material.SetFloat("_SquaresTile", 70f);
                renderer.material.SetFloat("_SquaresSpeed", 15f);
            }
            prefab.GetComponentInChildren<Light>().color = new Color(1f, 0f, 0f);
            Main.IonCubeCraftModelFix(prefab);
            
            prefab.EnsureComponent<Battery>()._capacity = 3000000;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new (Main.assetBundle.LoadAsset<Sprite>("RedIonCube_Icon"));
        }
    }
}
