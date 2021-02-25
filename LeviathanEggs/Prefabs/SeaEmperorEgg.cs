using System.Collections.Generic;
using ECCLibrary;
using UnityEngine;
using SMLHelper.V2.Handlers;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Prefabs
{
    public class SeaEmperorEgg : CreatureEggAsset
    {
        public SeaEmperorEgg()
            : base("SeaEmperorEgg", "Creature Egg", "An unknown Creature hatches from this", 
                  Main.assetBundle.LoadAsset<GameObject>("SeaEmperorEgg.prefab"),
                  TechType.SeaEmperorBaby, null, 5f)
        {
            OnFinishedPatching += () =>
            {
                SpriteHandler.RegisterSprite(this.TechType, Main.assetBundle.LoadAsset<Sprite>("SeaEmperorEgg"));
            };
        }
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
        public override void AddCustomBehaviours()
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

            prefab.GetComponent<Rigidbody>().mass = 100f;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
            resourceTracker.rb = prefab.GetComponent<Rigidbody>();
            resourceTracker.prefabIdentifier = prefab.GetComponent<PrefabIdentifier>();
            resourceTracker.pickupable = prefab.GetComponent<Pickupable>();

            prefab.AddComponent<SpawnLocations>();
        }
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => false;
    }
}
