using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Handlers;
using RotA.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections;
using UWE;

namespace RotA.Prefabs.Modules
{
    public class ExosuitDashModule : VehicleUpgrade, IVehicleOnEquip
    {
        public ExosuitDashModule() 
            : base("ExosuitDashModule", "Prawn Suit Ion Dash Module",
        "Allows the pilot to dash quickly in any direction, utilizing the Prawn Suit's built-in thrusters. Doesn't stack.")
        {
            OnFinishedPatching += () =>
            {
                KnownTechHandler.SetAnalysisTechEntry(TechType, new TechType[0],
                    UnlockSprite: Mod.assetBundle.LoadAsset<Sprite>("AlienUpgrade_Popup"));
            };
        }

        public override ModuleEquipmentType EquipmentType => ModuleEquipmentType.ExosuitModule;

        public override QuickSlotType QuickSlotType => QuickSlotType.Selectable;

        public override TechType ModelTemplate => TechType.ExoHullModule1;

        public override float? EnergyCost => 4f;

        public override float CraftingTime => 7f;

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override bool UnlockedAtStart { get; } = false;

        #region Interface Implementation

        public void OnEquip(int slotID, bool equipped, Vehicle vehicle)
        {
            if (vehicle is Exosuit exosuit)
            {
                if (equipped)
                {
                    DashOnKeyPress dash = exosuit.gameObject.EnsureComponent<DashOnKeyPress>();
                }
                else
                {
                    Object.Destroy(exosuit.gameObject.GetComponent<DashOnKeyPress>());
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
                    new Ingredient(AUHandler.CobaltIngotTechType, 1),
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(AUHandler.ElectricubeTechType, 1)
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("IonDash_Icon"));
        }

        protected override void CustomizePrefab(GameObject prefab)
        {
            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>(true);
            FixVFXFabricating(vfxFabricating);
            vfxFabricating.posOffset = new Vector3(0f, 0.05f, 0f);
            Mod.ApplyAlienUpgradeMaterials(prefab.GetComponentInChildren<Renderer>());
        }
    }
}
