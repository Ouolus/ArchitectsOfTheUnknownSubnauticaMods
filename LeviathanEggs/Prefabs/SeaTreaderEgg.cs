using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class SeaTreaderEgg : EggPrefab
    {
        public SeaTreaderEgg()
            : base("SeaTreaderEgg", "Sea Treader Egg", "Sea Treaders hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }

        public override GameObject Model => LoadGameObject("SeaTreaderEgg.prefab");
        
        public override TechType HatchingCreature => TechType.SeaTreader;
        
        public override float HatchingTime => 5f;
        
        public override Sprite ItemSprite => LoadSprite("SeaTreaderEgg");
        
        public override Vector2int SizeInInventory { get; } = new(3, 3);

        void LateEnhance(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>();

            renderer.material.SetFloat(ShaderPropertyID._GlowStrength, .2f);
            renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, .2f);

            prefab.AddComponent<SpawnLocations>();
        }
    }
}