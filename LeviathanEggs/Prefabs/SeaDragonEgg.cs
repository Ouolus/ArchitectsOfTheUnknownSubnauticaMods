using UnityEngine;
using ECCLibrary;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using LeviathanEggs.MonoBehaviours;
using LeviathanEggs.Prefabs.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs.Prefabs
{
    class SeaDragonEgg : EggPrefab
    {
        public SeaDragonEgg()
            :base("SeaDragonEgg", "Sea Dragon Egg", "Sea Dragons Hatch from these")
        {}

        public override GameObject Model => LoadGameObject("SeaDragonEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaDragon;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("SeaDragonEgg");

        public override OverrideTechType MakeATechTypeToOverride => new OverrideTechType("SeaDragonEggUndiscovered",
            "Creature Egg", "An unknown Creature Hatches from this");
        public override bool AcidImmune => true;
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Ceiling,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Lava,
                count = 1,
                probability = 0.25f,
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                count = 1,
                probability = 0.15f
            }
        };

        public override GameObject GetGameObject()
        {
            var prefab = base.GetGameObject();
            
            GameObject seaDragonEgg = Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell");
            Renderer[] aRenderers = seaDragonEgg.GetComponentsInChildren<Renderer>();
            Material shell = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderers)
            {
                if (renderer.name.StartsWith("Creatures_eggs_17"))
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
                renderer.material = shell;
            }
            seaDragonEgg.SetActive(false);

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
