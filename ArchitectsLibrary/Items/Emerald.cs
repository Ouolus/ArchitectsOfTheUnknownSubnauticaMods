namespace ArchitectsLibrary.Items
{
    using System.Collections.Generic;
    using UnityEngine;
    using UWE;
    using API;
    
    class Emerald : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "6e7f3d62-7e76-4415-af64-5dcd88fc3fe4";

        public Emerald() : base("Emerald", "Emerald", "Be₃Al₂SiO₆. Rare mineral with applications in advanced alien fabrication.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Emerald_Diffuse"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Emerald_Illum"));
            renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
            renderer.material.SetColor("_SpecColor", new Color(1f, 2f, 1.2f));
            renderer.material.SetFloat("_Fresnel", 0.6f);
            renderer.material.SetFloat("_SpecInt", 20f);
            renderer.material.SetFloat("_GlowStrength", 1f);
            renderer.material.SetFloat("_GlowStrengthNight", 1f);
            renderer.transform.localScale = Vector3.one * 0.66f;
            ApplyTranslucency(renderer);
            prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.glass;
        }

        private void ApplyTranslucency(Renderer renderer)
        {
            renderer.material.EnableKeyword("_ZWRITE_ON");
            renderer.material.EnableKeyword("WBOIT");
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.SetInt("_Cutoff", 0);
            renderer.material.SetFloat("_SrcBlend", 1f);
            renderer.material.SetFloat("_DstBlend", 1f);
            renderer.material.SetFloat("_SrcBlend2", 0f);
            renderer.material.SetFloat("_DstBlend2", 10f);
            renderer.material.SetFloat("_AddSrcBlend", 1f);
            renderer.material.SetFloat("_AddDstBlend", 1f);
            renderer.material.SetFloat("_AddSrcBlend2", 0f);
            renderer.material.SetFloat("_AddDstBlend2", 10f);
            renderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack | MaterialGlobalIlluminationFlags.RealtimeEmissive;
            renderer.material.renderQueue = 3101;
            renderer.material.enableInstancing = true;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("Emerald_Icon"));
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
                biome = BiomeType.SeaTreaderPath_Rock,
                count = 1,
                probability = 0.2f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.SeaTreaderPath_Sand,
                count = 1,
                probability = 0.07f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.BloodKelp_TrenchFloor,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.BloodKelp_Grass,
                count = 1,
                probability = 0.15f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.BloodKelp_Wall,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 0.13f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_CaveFloor,
                count = 1,
                probability = 0.5f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_CaveWall,
                count = 1,
                probability = 0.5f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_RockWall,
                count = 1,
                probability = 0.3f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_SandDune,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_SandPlateau,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_CaveCeiling,
                count = 1,
                probability = 1.2f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_CaveWall,
                count = 1,
                probability = 1.2f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_CaveFloor,
                count = 1,
                probability = 1.2f
            },
        };
    }
}
