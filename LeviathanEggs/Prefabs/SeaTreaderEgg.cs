using System.Collections.Generic;
using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class SeaTreaderEgg : LeviathanEgg
    {
        public SeaTreaderEgg()
            : base("SeaTreaderEgg", "Sea Treader Egg", "Sea Treaders hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }

        public override GameObject Model => LoadGameObject("SeaTreaderEgg.prefab");
        
        public override TechType HatchingCreature => TechType.SeaTreader;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.SeaTreaderPath_Path,
                probability = 0.05f
            }
        };
        
        public override Sprite ItemSprite => LoadSprite("SeaTreaderEgg");
        
        void LateEnhance(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>();

            renderer.material.SetFloat(ShaderPropertyID._GlowStrength, .2f);
            renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, .2f);

            prefab.AddComponent<SpawnLocations>();
        }
    }
}