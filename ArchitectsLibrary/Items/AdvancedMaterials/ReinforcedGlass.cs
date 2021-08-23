namespace ArchitectsLibrary.Items.AdvancedMaterials
{
    using SMLHelper.V2.Crafting;
    using System.Collections.Generic;
    using API;
    using Handlers;
    using UnityEngine;
    
    class ReinforcedGlass : ReskinSpawnable
    {
        public ReinforcedGlass() : base("ReinforcedGlass", LanguageSystem.Get("ReinforcedGlass"), LanguageSystem.GetTooltip("ReinforcedGlass"))
        {
            OnFinishedPatching += () =>
            {
                AUHandler.ReinforcedGlassTechType = TechType;
                CraftData.pickupSoundList.Add(TechType, "event:/loot/pickup_glass");
            };
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;
        
        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "AdvancedMaterials" };

        protected override string ReferenceClassId => "7965512f-39fe-4770-9060-98bf149bca2e";

        public override float CraftingTime => 2f;

        public override TechType RequiredForUnlock => AUHandler.SapphireTechType;

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new(AUHandler.SapphireTechType, 2)
                }
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>(true);
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Material_Reinforced_Glass_specular"));
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("Reinforced_glass"));
        }
    }
}
