using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class GhostRayEgg : EggPrefab
    {
        public GhostRayEgg()
            : base("GhostRayEgg", "Ghost Ray Egg", "Ghost Rays hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("GhostRayEgg.prefab");
        
        public override TechType HatchingCreature => TechType.GhostRayBlue;
        
        public override float HatchingTime => 5f;
        
        public override Sprite ItemSprite => LoadSprite("GhostRayEgg");
        
        public override Vector2int SizeInInventory { get; } = new(2, 2);

        void LateEnhance(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                var material = renderer.material;
                if (material.name.Contains("Transparent"))
                {
                    material.SetFloat("_Shininess", 3f);
                    material.SetFloat("_SpecInt", 3f);
                    break;
                }
            }
            prefab.AddComponent<SpawnLocations>();
        }
    }
}