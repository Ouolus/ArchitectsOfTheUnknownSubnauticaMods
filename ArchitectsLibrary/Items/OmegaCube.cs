namespace ArchitectsLibrary.Items 
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using API;
    using UnityEngine;
    
    public class OmegaCube : ReskinSpawnable
    {
        public OmegaCube() : base("OmegaCube", "Omega cube", "Complex alien material with gargantuan energy capacity. Applications in warp drive technology.")
        {
            OnFinishedPatching += () =>
            {
                DisplayCaseServices.WhitelistTechType(TechType);
                DisplayCaseServices.SetOffset(TechType, new Vector3(0f, -0.25f, 0f));
            };
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

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
            return new(Main.assetBundle.LoadAsset<Sprite>("OmegaCube_Icon"));
        }

        protected override string ReferenceClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";
    }
}