namespace ArchitectsLibrary.Items
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using UnityEngine;
    using Handlers;
    using API;
    
    class RedIonCube : PrecursorIonCube
    {
        public RedIonCube() : base("RedIonCube", "Power cube", "A high capacity energy source with a similar structure to the Ion Cube. Capable of releasing massive amounts of energy in a short burst. Applications in powerful offensive and defensive technology.")
        {
        }

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

        protected override int Capacity => 3000000;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach(var renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                renderer.material.SetColor("_Color", new Color(1f, 0.2f, 0f));
                renderer.material.SetColor("_SpecColor", new Color(1f, 0.2f, 0f));
                renderer.material.SetColor("_DetailsColor", new Color(1f, 1f, 1f));
                renderer.material.SetColor("_SquaresColor", new Color(0.5f, 0.5f, 0.5f));
                renderer.material.SetFloat("_SquaresTile", 70f);
                renderer.material.SetFloat("_SquaresSpeed", 15f);
            }
            prefab.GetComponentInChildren<Light>().color = new Color(1f, 0f, 0f);
            
            base.ApplyChangesToPrefab(prefab);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new (Main.assetBundle.LoadAsset<Sprite>("RedIonCube_Icon"));
        }
    }
}
