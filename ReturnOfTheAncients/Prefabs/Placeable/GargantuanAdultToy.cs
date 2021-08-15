using ArchitectsLibrary.API;
using ArchitectsLibrary.Utility;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace RotA.Prefabs.Placeable
{
    class GargantuanAdultToy : ALPlaceable
    {
        public GargantuanAdultToy()
            : base("GargantuanAdultToy", "Gargantuan Adult Replica (With hat)", "A small-scale replica of an Adult Gargantuan Leviathan. Equipped with a cute hat to make it less terrifying.")
        {}


        public override PlacementFlags GetPlacementMode => PlacementFlags.Inside | PlacementFlags.AllowedOnRigidbody | PlacementFlags.Ground | PlacementFlags.AllowedOnConstructable;

        protected override TechData GetBlueprintRecipe() => new()
        {
            craftAmount = 1,
            Ingredients =
            {
                new(TechType.Quartz, 2),
                new(TechType.Titanium, 1),
                new(TechType.CreepvineSeedCluster, 1)
            }
        };

        protected override Atlas.Sprite GetItemSprite() => new(Mod.gargAssetBundle.LoadAsset<Sprite>("GargAdultToy_Icon"));

        public override GameObject GetModel()
        {
            GameObject model = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("GargantuanAdultToy"));
            MaterialUtils.ApplySNShaders(model);
            return model;
        }
    }
}