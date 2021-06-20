using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class RiverProwlerEgg : EggPrefab
    {
        public RiverProwlerEgg()
            : base("RiverProwlerEgg", "River Prowler Egg", "River Prowlers hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("RiverProwlerEgg.prefab");
        
        public override TechType HatchingCreature => TechType.SpineEel;
        
        public override float HatchingTime => 2f;
        
        public override Sprite ItemSprite => LoadSprite("ProwlerEgg");
        
        void LateEnhance(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                var material = renderer.material;
                if (material.name.Contains("Transparent"))
                {
                    material.SetFloat("_Shininess", 2f);
                    material.SetFloat("_SpecInt", 5f);
                    break;
                }
            }
            prefab.AddComponent<SpawnLocations>();
        }

        public override Vector2int SizeInInventory => new(2, 2);
    }
}
