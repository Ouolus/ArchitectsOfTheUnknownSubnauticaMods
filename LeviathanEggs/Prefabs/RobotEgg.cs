using System.Collections.Generic;
using System.IO;
using SMLHelper.V2.Utility;
using ECCLibrary;
using UnityEngine;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Prefabs
{
    class RobotEgg : CreatureEggAsset
    {
        public RobotEgg()
            :base("RobotEgg", "Robot Egg", "Robot Egg that makes me go yes", Main.assetBundle.LoadAsset<GameObject>("RobotEgg.prefab"), TechType.PrecursorDroid, null, 2f)
        {
        }
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
        public override Vector2int SizeInInventory => new Vector2int(2, 2);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => false;
        public override void AddCustomBehaviours()
        {
            Material material = new Material(Shader.Find("MarmosetUBER"))
            {
                mainTexture = Main.assetBundle.LoadAsset<Texture2D>("RobotEggDiffuse"),
            };
            material.EnableKeyword("MARMO_NORMALMAP");
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("MARMO_EMISSION");

            material.SetTexture(ShaderPropertyID._Illum, Main.assetBundle.LoadAsset<Texture2D>("RobotEggIllum"));
            material.SetTexture(ShaderPropertyID._SpecTex, Main.assetBundle.LoadAsset<Texture2D>("RobotEggDiffuse"));
            material.SetTexture(ShaderPropertyID._BumpMap, Main.assetBundle.LoadAsset<Texture2D>("RobotEggNormal"));

            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
            {
                rend.material = material;
                rend.sharedMaterial = material;
            }

            prefab.GetComponent<Rigidbody>().mass = 100f;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
            resourceTracker.rb = prefab.GetComponent<Rigidbody>();
            resourceTracker.prefabIdentifier = prefab.GetComponent<PrefabIdentifier>();
            resourceTracker.pickupable = prefab.GetComponent<Pickupable>();

            prefab.AddComponent<SpawnLocations>();
        }
    }
}
