namespace ArchitectsLibrary.Items.Cubes 
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using API;
    using UnityEngine;
    
    class OmegaCube : PrecursorIonCube
    {
        public OmegaCube() : base("OmegaCube", "Omega cube", "Complex alien material with gargantuan energy capacity. Applications in warp drive technology.")
        {
            OnFinishedPatching += () =>
            {
                DisplayCaseServices.WhitelistTechType(TechType);
                DisplayCaseServices.SetOffset(TechType, new Vector3(0f, -0.25f, 0f));
            };
        }

        protected override int Capacity => 4000000;

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            foreach (var renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                renderer.material.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f));
                renderer.material.SetColor("_SpecColor", new Color(1f, 1f, 1f));
                renderer.material.SetColor("_DetailsColor", new Color(1f, 2f, 1.25f));
                renderer.material.SetColor("_SquaresColor", new Color(0.5f, 0.5f, 0.5f));
            }
            prefab.GetComponentInChildren<Light>().color = new Color(0.8f, 1f, 1f);
            
            base.ApplyChangesToPrefab(prefab);
        }

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
            return new(Main.assetBundle.LoadAsset<Sprite>("OmegaCube_Icon"));
        }
    }
}