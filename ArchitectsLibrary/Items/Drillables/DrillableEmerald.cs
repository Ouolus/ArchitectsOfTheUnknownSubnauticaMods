using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Items.Drillables
{
    using UnityEngine;
    using API;
    using Handlers;
    using System.Collections.Generic;
    using UWE;
    
    internal class DrillableEmerald : ReskinSpawnable
    {
        protected override string ReferenceClassId => "4f441e53-7a9a-44dc-83a4-b1791dc88ffd";

        public DrillableEmerald() : base("DrillableEmerald", "Emerald", "Be₃Al₂SiO₆. Beryl variant with applications in advanced alien fabrication.")
        {
            OnFinishedPatching += () =>
            {
                AUHandler.DrillableEmeraldTechType = TechType;
                ItemUtils.MakeObjectScannable(TechType, Main.encyKey_emerald, 5f);
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.GetComponentInChildren<Light>().color = Color.green; 
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.EmeraldTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.EmeraldTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 2;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            renderers[0].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_med_01"));
            renderers[0].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_med_01_emissive"));
            renderers[1].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_med_02"));
            renderers[1].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_med_02_emissive"));
            renderers[2].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_01"));
            renderers[2].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_01_emissive"));
            renderers[3].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_05"));
            renderers[3].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_05_emissive"));
            renderers[4].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_04"));
            renderers[4].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_04_emissive"));
            renderers[5].material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_02"));
            renderers[5].material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("kyanite_small_02_emissive"));
            foreach (var renderer in renderers)
            {
                renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
                renderer.material.SetColor("_SpecColor", new Color(1f, 2f, 1.2f));
                renderer.material.SetFloat("_Fresnel", 0.6f);
                renderer.material.SetFloat("_SpecInt", 20f);
                renderer.material.SetFloat("_GlowStrength", 1f);
                renderer.material.SetFloat("_GlowStrengthNight", 1f);
                ApplyTranslucency(renderer);
            }
        }

        void ApplyTranslucency(Renderer renderer)
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

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Medium,
            techType = TechType
        };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new()
        {
            new()
            {
                biome = BiomeType.CragField_Ground,
                count = 1,
                probability = 0.015f
            },
            new()
            {
                biome = BiomeType.CragField_Ground,
                count = 1,
                probability = 0.015f
            },
            new()
            {
                biome = BiomeType.Dunes_SandDune,
                count = 1,
                probability = 0.0375f
            },
            new()
            {
                biome = BiomeType.Dunes_SandPlateau,
                count = 1,
                probability = 0.0375f
            }
        };
    }
}
