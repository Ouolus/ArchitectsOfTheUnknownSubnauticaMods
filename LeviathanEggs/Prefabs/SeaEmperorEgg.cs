using System.Collections.Generic;
using UnityEngine;
using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs.Prefabs
{
    public class SeaEmperorEgg : LeviathanEgg
    {
        public SeaEmperorEgg()
            : base("SeaEmperorEgg", "Sea Emperor Egg", "Sea Emperors hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }
        public override GameObject Model => LoadGameObject("SeaEmperorEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaEmperorBaby;
        public override Sprite ItemSprite => LoadSprite("SeaEmperorEgg");
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new ()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.PrisonAquarium_CaveFloor,
                count = 1,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.PrisonAquarium_Grass,
                count = 1,
                probability = 0.01f
            },
        };

        public void InitializeObject(GameObject prefab)
        {
            GameObject seaEmperorEgg = Resources.Load<GameObject>("WorldEntities/Eggs/EmperorEgg");
            Renderer[] aRenderer = seaEmperorEgg.GetComponentsInChildren<Renderer>();
            Material shell = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderer)
            {
                if (renderer.name.StartsWith("Creatures_egg_11"))
                {
                    shell = renderer.material;
                    break;
                }
                if (shell != null)
                    break;
            }
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material.shader = shader;
                renderer.material = shell;
                renderer.sharedMaterial = shell;
            }
            seaEmperorEgg.SetActive(false);

            prefab.AddComponent<SpawnLocations>();
        }
    }
}
