using System.Collections.Generic;
using ECCLibrary;
using UnityEngine;
using SMLHelper.V2.Handlers;
using LeviathanEggs.MonoBehaviours;
using LeviathanEggs.Prefabs.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs.Prefabs
{
    public class SeaEmperorEgg : EggPrefab
    {
        public SeaEmperorEgg()
            : base("SeaEmperorEgg", "Sea Emperor Egg", "Sea Emperors Hatch from these")
        {}
        public override GameObject Model => LoadGameObject("SeaEmperorEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaEmperorBaby;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("SeaEmperorEgg");
        public override bool AcidImmune => true;
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.PrisonAquarium_CaveFloor,
                count = 1,
                probability = 0.5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.PrisonAquarium_Grass,
                count = 1,
                probability = 0.1f
            },
        };

        public override GameObject GetGameObject()
        {
            var prefab = base.GetGameObject();
            
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
