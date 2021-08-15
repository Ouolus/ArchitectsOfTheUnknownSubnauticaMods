using ArchitectsLibrary.API;
using ArchitectsLibrary.Utility;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace RotA.Prefabs.Placeable
{
    class GargantuanAdultToyNoHat : ALPlaceable
    {
        public GargantuanAdultToyNoHat()
            : base("GargantuanAdultToyNoHat", "Gargantuan Adult Replica", "A small-scale replica of an Adult Gargantuan Leviathan. Approximately 0.16% the size of the actual creature.")
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
            GameObject model = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("GargantuanAdultToyNoHat"));
            MaterialUtils.ApplySNShaders(model);
            return model;
        }
    }
}