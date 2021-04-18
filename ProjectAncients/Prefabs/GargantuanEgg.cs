using ArchitectsLibrary.API;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanEgg : EggPrefab
    {
        public GargantuanEgg()
            : base("GargantuanEgg", "Gargantuan Egg", "Gargantuan Leviathans hatch from these.")
        {}

        public override TechType HatchingCreature => Mod.gargBabyPrefab.TechType;
        public override float HatchingTime => 1f;
        public override Sprite ItemSprite => null;
        public override Texture2D ItemSpriteFromTexture => Mod.assetBundle.LoadAsset<Texture2D>("GargEgg_Icon");
        public override GameObject Model => Mod.assetBundle.LoadAsset<GameObject>("GargEgg_Prefab");
        public override bool MakeCreatureLayEggs => true;
        public override bool AcidImmune => true;
        public override float MaxHealth => 100f;
        public override float Mass => 500f;
        public override bool MakeObjectScannable => true;
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
    }
}