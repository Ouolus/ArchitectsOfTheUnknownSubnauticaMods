using System.Collections.Generic;
using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class GhostRayEgg : EggPrefab
    {
        public GhostRayEgg()
            : base("GhostRayEgg", "Ghost Ray Egg", "Ghost Rays hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("GhostRayEgg.prefab");
        
        public override TechType HatchingCreature => TechType.GhostRayBlue;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.GhostTree_Ground,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverJunction_Ground,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverCorridor_Ground,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverCorridor_LakeFloor,
                probability = 0.01f
            }
        };

        public override float HatchingTime => 5f;
        
        public override Sprite ItemSprite => LoadSprite("GhostRayEgg");
        
        public override Vector2int SizeInInventory { get; } = new(2, 2);

        void LateEnhance(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                var material = renderer.material;
                if (material.name.Contains("2"))
                {
                    material.SetFloat("_Shininess", 8f);
                    material.SetFloat("_SpecInt", 3f);
                }
                else if (!material.name.Contains("2"))
                {
                    material.SetFloat("_Shininess", 6f);
                    material.SetFloat("_SpecInt", 5f);
                    material.SetFloat("_EmissionLMNight", .03f);
                }
            }
            prefab.AddComponent<SpawnLocations>();
        }
    }
}