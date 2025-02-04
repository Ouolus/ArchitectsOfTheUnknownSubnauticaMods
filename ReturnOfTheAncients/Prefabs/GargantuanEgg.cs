using ArchitectsLibrary.API;
using UnityEngine;

namespace RotA.Prefabs
{
    public class GargantuanEgg : EggPrefab
    {
        public GargantuanEgg()
            : base("GargantuanEgg", LanguageSystem.Get("GargantuanEgg"), LanguageSystem.GetTooltip("GargantuanEgg"))
        { LateEnhancements += InitializeObject; }

        public override TechType HatchingCreature => Mod.gargBabyPrefab.TechType;
        public override float HatchingTime => 1f;
        public override Sprite ItemSprite => Mod.gargAssetBundle.LoadAsset<Sprite>("GargEgg_Icon");
        public override GameObject Model => Mod.gargAssetBundle.LoadAsset<GameObject>("GargEgg_Prefab");
        public override bool MakeCreatureLayEggs => true;
        public override bool AcidImmune => true;
        public override float MaxHealth => 100f;
        public override float Mass => 500f;
        public override bool MakeObjectScannable => true;
        public override Vector2int SizeInInventory => new Vector2int(5, 5);
        public override int RequiredACUSize => 3;

        public void InitializeObject(GameObject prefab)
        {
            var inspect = prefab.AddComponent<InspectOnFirstPickup>();
            inspect.pickupAble = prefab.GetComponent<Pickupable>();
            inspect.collision = prefab.GetComponent<Collider>();
            inspect.rigidBody = prefab.GetComponent<Rigidbody>();
            inspect.animParam = "holding_precursorioncrystal";
            inspect.inspectDuration = 4.1f;
            prefab.GetComponent<CreatureEgg>().explodeOnHatch = false;
            foreach(Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                renderer.material.SetFloat("_Fresnel", 0.5f);
                renderer.material.SetFloat("_SpecInt", 8f);
            }
        }
    }
}
