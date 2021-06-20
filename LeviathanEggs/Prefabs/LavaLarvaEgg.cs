using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class LavaLarvaEgg : EggPrefab
    {
        public LavaLarvaEgg()
            : base("LavaLarvaEgg", "LavaLarva Egg", "LavaLarva hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("LavaLarvaEgg.prefab");
        
        public override TechType HatchingCreature => TechType.LavaLarva;
        
        public override float HatchingTime => 5f;
        
        public override Sprite ItemSprite => LoadSprite("LavaLarvaEgg");
        
        public override Vector2int SizeInInventory { get; } = new(1, 1);

        void LateEnhance(GameObject prefab)
        {
            var renderer = prefab.GetComponentInChildren<Renderer>();
            
            renderer.material.SetFloat(ShaderPropertyID._GlowStrength, 0.3f);
            renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, 0.3f);
            
            prefab.AddComponent<SpawnLocations>();
        }
    }
}