using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using ProjectAncients.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections;
using UWE;

namespace ProjectAncients.Prefabs.Modules
{
    public class ExosuitZapModule : VehicleUpgrade, IVehicleOnEquip
    {
        public ExosuitZapModule()
    : base("ExosuitZapModule", "Prawn Suit Ion Defense Module",
        "When taking damage, it generates a small electrical pulse designed to ward off aggressive fauna. Generates an ionic energy pulse if necessary. Doesn't stack.")
        {

        }

        public override ModuleEquipmentType EquipmentType => ModuleEquipmentType.ExosuitModule;

        public override QuickSlotType QuickSlotType => QuickSlotType.Selectable;

        public override TechType ModelTemplate => TechType.ExoHullModule1;

        public override float? EnergyCost => 4f;

        public override float CraftingTime => 7f;

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override TechType RequiredForUnlock => Mod.architectElectricityMasterTech;

        #region Interface Implementation

        public void OnEquip(int slotID, bool equipped, Vehicle vehicle)
        {
            if (vehicle is Exosuit exosuit)
            {
                if (equipped)
                {
                    ZapOnDamage zod = exosuit.gameObject.EnsureComponent<ZapOnDamage>();
                    zod.zapPrefab = Mod.electricalDefensePrefab;
                }
                else
                {
                    ZapOnDamage zod = exosuit.gameObject.GetComponent<ZapOnDamage>();
                    if (zod)
                    {
                        Object.Destroy(zod);
                    }
                }
            }
        }

        #endregion

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.TitaniumIngot, 1),
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.PrecursorIonCrystal, 2),
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("PrawnSuitAutoZapper"));
        }
    }
}
