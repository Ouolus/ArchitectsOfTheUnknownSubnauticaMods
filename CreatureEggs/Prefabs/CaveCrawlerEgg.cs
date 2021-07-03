using System.Collections.Generic;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace CreatureEggs.Prefabs
{
    public class CaveCrawlerEgg : EggPrefab
    {
        public CaveCrawlerEgg()
            : base("CaveCrawlerEgg", "Cave Crawler Egg", "Cave Crawlers hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("CaveCrawlerEgg.prefab");
        
        public override TechType HatchingCreature => TechType.CaveCrawler;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.FloatingIslands_Shallows,
                probability = 0.02f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.FloatingIslands_AbandonedBase_Outside,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.Mountains_IslandGrass,
                probability = 0.01f,
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.Mountains_IslandSand,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.Dunes_CaveFloor,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.UnderwaterIslands_IslandCaveFloor,
                probability = 0.01f
            }
        };

        public override float HatchingTime => 2f;
        
        public override Sprite ItemSprite => LoadSprite("CaveCrawlerEgg");
    }
}