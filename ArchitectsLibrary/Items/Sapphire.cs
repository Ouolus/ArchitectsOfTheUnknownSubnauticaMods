using System.Collections.Generic;
using SMLHelper.V2.Utility;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items
{
    class Sapphire : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "87293f19-cca3-46e6-bb3d-6e8dc579e27b";

        public Sapphire() : base("Sapphire", "Sapphire", "Al₂O₃. Valuable insulative properties and applications in glass reinforcement.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_spec"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_illum"));
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            if (sprite == null)
            {
                sprite = ImageUtils.LoadSpriteFromTexture(Main.assetBundle.LoadAsset<Texture2D>("Sapphire_icon"));
            }
            return sprite;
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
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_Ground,
                count = 1,
                probability = 0.65f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_WhiteCoral,
                count = 1,
                probability = 0.3f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.DeepGrandReef_Ceiling,
                count = 1,
                probability = 0.25f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.DeepGrandReef_Ground,
                count = 1,
                probability = 0.6f
            },
        };
    }
}
