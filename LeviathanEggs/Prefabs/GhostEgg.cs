using System.Collections.Generic;
using SMLHelper.V2.Handlers;
using ECCLibrary;
using UnityEngine;
using System;
using LeviathanEggs.MonoBehaviours;
using LeviathanEggs.Prefabs.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs.Prefabs
{
    class GhostEgg : EggPrefab
    {
        public GhostEgg()
            : base("GhostEgg", "Ghost Leviathan Egg", "Ghosts Hatch from these.")
        {}

        public override GameObject Model => LoadGameObject("GhostEgg.prefab");
        public override OverrideTechType MakeATechTypeToOverride =>
            new OverrideTechType("GhostEggUndiscovered", "Creature Egg", "An unknown Creature hatches from this");
        public override TechType HatchingCreature => TechType.GhostLeviathanJuvenile;
        public override Sprite ItemSprite => LoadSprite("GhostEgg");
        public override float HatchingTime => 5f;
        public override bool AcidImmune => true;
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_LakeFloor,
                count = 1,
                probability = 0.2f,
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_Ground,
                count = 1,
                probability = 0.4f
            }
        };

        public override GameObject GetGameObject()
        {
            var prefab = base.GetGameObject();
            
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
            if (renderers == null)
            {
                ErrorMessage.AddMessage("renderer is null");
                Console.WriteLine("Renderer is null");
            }
            if (shell == null)
            {
                ErrorMessage.AddMessage("shell material is null");
                Console.WriteLine("Shell material is null");
            }
            if (embryo == null)
            {
                ErrorMessage.AddMessage("embryo material is null");
                Console.WriteLine("Embryo material is null");
            }

            ghostEgg.SetActive(false);
            
            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
            resourceTracker.rb = prefab.GetComponent<Rigidbody>();
            resourceTracker.prefabIdentifier = prefab.GetComponent<PrefabIdentifier>();
            resourceTracker.pickupable = prefab.GetComponent<Pickupable>();

            prefab.AddComponent<SpawnLocations>();

            return prefab;
        }

        public override Vector2int SizeInInventory => new Vector2int(3, 3);
    }
}
