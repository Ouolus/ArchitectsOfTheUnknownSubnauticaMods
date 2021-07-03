using System.Collections.Generic;
using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class CrimsonRayEgg : EggPrefab
    {
        public CrimsonRayEgg()
            : base("CrimsonRayEgg", "Crimson Ray Egg", "Crimson Rays hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        public override GameObject Model => LoadGameObject("CrimsonRayEgg.prefab");
        
        public override TechType HatchingCreature => TechType.GhostRayRed;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.InactiveLavaZone_Chamber_Floor,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.InactiveLavaZone_Corridor_Floor,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.InactiveLavaZone_CastleChamber_Floor,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                probability = 0.01f
            }
        };

        public override float HatchingTime => 5f;
        
        public override Sprite ItemSprite => LoadSprite("CrimsonRayEgg");
        
        public override Vector2int SizeInInventory { get; } = new(2, 2);

        void LateEnhance(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetFloat("_EmissionLMNight", 0.001f);
            
            prefab.AddComponent<SpawnLocations>();
        }
    }
}