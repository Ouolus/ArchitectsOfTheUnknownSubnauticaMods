namespace ArchitectsLibrary.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;
    using UnityEngine;
    using UWE;
    using API;
    using Handlers;
    
    class CobaltIngot : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "4ae90608-40da-45ce-8480-e2f0133f96b2";

        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "AdvancedMaterials" };

        public CobaltIngot() : base("CobaltIngot", "Cobalt ingot", "Co. Condensed cobalt bar.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltIngot_Diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltIngot_Spec"));
            renderer.material.SetColor("_SpecColor", new Color(2f, 2f, 2f));
            renderer.material.SetColor("_Color", new Color(5f, 5f, 5f));
            renderer.material.SetFloat("_Shininess", 5f);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("CobaltIngot_Icon"));
        }

        protected override TechData GetBlueprintRecipe()
        {
            TechData techData = new TechData(new List<Ingredient>() { new Ingredient(AUHandler.CobaltTechType, 5) });
            techData.craftAmount = 1;
            return techData;
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Near,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Small,
            techType = TechType
        };
    }
}
