using System.Collections.Generic;
using UnityEngine;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
namespace CreatureEggs.Prefabs
{
    class RobotEgg : EggPrefab
    {
        public RobotEgg()
            :base("RobotEgg", "Alien Robot Egg", "Alien Robots are deployed from these.")
        {
            EarlyEnhancements += InitializeObject;
        }
        public override OverrideTechType MakeATechTypeToOverride =>
            new OverrideTechType("RobotEggUndiscovered", "Unknown Alien Artifact", "Unknown Alien technology that appears to store some kind of device.");
        public override GameObject Model => LoadGameObject("RobotEgg.prefab");
        public override TechType HatchingCreature => TechType.PrecursorDroid;
        public override float HatchingTime => 3f;
        public override Sprite ItemSprite => LoadSprite("RobotEgg");
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

        public void InitializeObject(GameObject prefab)
        {
            Material material = new Material(Shader.Find("MarmosetUBER"))
            {
                mainTexture = LoadTexture2D("RobotEggDiffuse"),
            };
            material.EnableKeyword("MARMO_NORMALMAP");
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("MARMO_EMISSION");

            material.SetTexture(ShaderPropertyID._Illum, LoadTexture2D("RobotEggIllum"));
            material.SetTexture(ShaderPropertyID._SpecTex, LoadTexture2D("RobotEggDiffuse"));
            material.SetTexture(ShaderPropertyID._BumpMap, LoadTexture2D("RobotEggNormal"));

            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
            {
                rend.material = material;
                rend.sharedMaterial = material;
            }

            prefab.AddComponent<SpawnLocations>();
            prefab.EnsureComponent<RobotEggPulsating>();
        }
        public override Vector2int SizeInInventory => new Vector2int(2, 2);
    }
}
