using System.Collections.Generic;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace CreatureEggs.Prefabs
{
    public class LavaLarvaEgg : EggPrefab
    {
        public LavaLarvaEgg()
            : base("LavaLarvaEgg", "LavaLarva Egg", "LavaLarva hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("LavaLarvaEgg.prefab");
        
        public override TechType HatchingCreature => TechType.LavaLarva;
        
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
        
        public override Sprite ItemSprite => LoadSprite("LavaLarvaEgg");
        
        public override Vector2int SizeInInventory { get; } = new(1, 1);

        void LateEnhance(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>();
            
            renderer.material.SetFloat(ShaderPropertyID._GlowStrength, 0.3f);
            renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, 0.3f);
            
            prefab.AddComponent<SpawnLocations>();
        }
    }
}