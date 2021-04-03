using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    [HarmonyPatch]
    public class ExosuitPatches
    {
        internal static Dictionary<TechType, IVehicleOnEquip> ExosuitOnEquips = new();
        internal static Dictionary<TechType, IVehicleOnToggleOnce> ExosuitOnToggleOnces = new();
        internal static Dictionary<TechType, IVehicleOnToggleRepeating> ExosuitOnToggleRepeatings = new();

        [HarmonyPatch(typeof(Vehicle))]
        [HarmonyPatch(nameof(Vehicle.OnUpgradeModuleToggle))]
        [HarmonyPrefix]
        static bool OnUpgradeModuleToggle_Postfix(Vehicle __instance, bool active, int slotID)
        {
            if(__instance is not Exosuit)
            {
                return true;
            }
            var techType = __instance.modules.GetTechTypeInSlot(__instance.slotIDs[slotID]);

            if (ExosuitOnToggleOnces.TryGetValue(techType, out IVehicleOnToggleOnce exosuitOnToggle))
            {
                exosuitOnToggle.OnToggleOnce(slotID, active, __instance);

                __instance.quickSlotTimeUsed[slotID] = Time.time;
            }
            if (ExosuitOnToggleRepeatings.TryGetValue(techType, out IVehicleOnToggleRepeating exosuitOnToggleRepeating))
            {
                var onToggles = __instance.gameObject.GetAllComponentsInChildren<VehicleOnToggleRepeating>();
                foreach (var toggle in onToggles)
                {
                    // if the component already exists on the seamoth, then skip adding it again
                    if (toggle.techType == techType)
                    {
                        toggle.enabled = active;
                        return false;
                    }
                }
                var onToggle = __instance.gameObject.AddComponent<VehicleOnToggleRepeating>();
                onToggle.vehicleOnToggle = exosuitOnToggleRepeating;
                onToggle.vehicle = __instance;
                onToggle.slotID = slotID;
                onToggle.techType = techType;
                onToggle.enabled = active;

                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(Vehicle))]
        [HarmonyPatch(nameof(Vehicle.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        static void OnUpgradeModuleChange(Vehicle __instance, TechType techType, int slotID, bool added)
        {
            if(__instance is not Exosuit)
            {
                return;
            }
            if (ExosuitOnEquips.TryGetValue(techType, out IVehicleOnEquip exosuitOnEquip))
            {
                exosuitOnEquip.OnEquip(slotID, added, __instance);
            }
        }
    }
}