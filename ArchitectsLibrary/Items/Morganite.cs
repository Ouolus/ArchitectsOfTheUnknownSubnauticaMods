namespace ArchitectsLibrary.Items
{
    using System.Collections.Generic;
    using UnityEngine;
    using UWE;
    using API;
    
    class Morganite : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "8ef17c52-2aa8-46b6-ada3-c3e3c4a78dd6";

        public Morganite() : base("Morganite", "Morganite", "Be₃Al₂SiO₆. Rare mineral with applications in advanced alien fabrication.")
        {

        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Morganite_diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Morganite_spec"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Morganite_illum"));
            renderer.material.SetColor("_GlowColor", new Color(2f, 0f, 2f));
            renderer.material.SetColor("_SpecColor", new Color(2f, 1.5f, 2f));
            renderer.material.SetFloat("_SpecInt", 2f);
            renderer.material.SetFloat("_Shininess", 7f);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("Morganite_icon"));
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Near,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Small,
            techType = TechType
        };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new()
        {
            new()
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 0.13f
            },
            new()
            {
                biome = BiomeType.KooshZone_CaveFloor,
                count = 1,
                probability = 0.5f
            },
            new()
            {
                biome = BiomeType.KooshZone_CaveWall,
                count = 1,
                probability = 0.5f
            },
            new()
            {
                biome = BiomeType.KooshZone_RockWall,
                count = 1,
                probability = 0.3f
            },
        };
    }
}
