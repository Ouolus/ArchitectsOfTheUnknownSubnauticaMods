namespace ArchitectsLibrary.Items
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using UnityEngine;
    using API;
    using Handlers;
    
    class Electricube : PrecursorIonCube
    {
        public Electricube() : base("Electricube", "Electricube", "A high capacity energy source with a similar structure to the Ion Cube. Has applications in biomechanical materials and warping technology.")
        {
        }

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

        protected override int Capacity => 2000000;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach(var renderer in prefab.GetComponentsInChildren<Renderer>(true))
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
            prefab.GetComponent<InspectOnFirstPickup>().animParam = "holding_precursorioncrystal";
            
            base.ApplyChangesToPrefab(prefab);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new (Main.assetBundle.LoadAsset<Sprite>("Electricube_Icon"));
        }
    }
}
