using System.Collections.Generic;
using UnityEngine;
using System;
using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs.Prefabs
{
    class GhostEgg : LeviathanEgg
    {
        public GhostEgg()
            : base("GhostEgg", "Ghost Leviathan Egg", "Ghosts hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }

        public override GameObject Model => LoadGameObject("GhostEgg.prefab");
        public override TechType HatchingCreature => TechType.GhostLeviathanJuvenile;
        public override Sprite ItemSprite => LoadSprite("GhostEgg");
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new ()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_LakeFloor,
                count = 1,
                probability = 0.02f,
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_Ground,
                count = 1,
                probability = 0.04f
            }
        };

        public void InitializeObject(GameObject prefab)
        {
            GameObject ghostEgg = Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01");
            Renderer[] aRenderer = ghostEgg.GetAllComponentsInChildren<Renderer>();
            Material shell = null;
            Material embryo = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderer)
            {
                if (renderer.name == "lost_river_cove_tree_01_eggs_shells")
                {
                    shell = renderer.material;
                    break;
                }
                if (shell != null)
                    break;
            }
            foreach (var renderer in aRenderer)
            {
                if (renderer.name == "lost_river_cove_tree_01_eggs")
                {
                    embryo = renderer.material;
                    break;
                }
                if (embryo != null)
                    break;
            }
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            Material[] materials = new Material[2] { shell, embryo };
            foreach(var renderer in renderers)
            {
                if (shell != null && embryo != null)
                {
                    renderer.material.shader = shader;
                    renderer.materials = materials;
                }
            }

            ghostEgg.SetActive(false);

            prefab.AddComponent<SpawnLocations>();
        }
    }
}
