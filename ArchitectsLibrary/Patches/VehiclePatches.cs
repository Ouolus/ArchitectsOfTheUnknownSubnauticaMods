using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Patches
{
    public class VehiclePatches
    {
        internal static Dictionary<TechType, ISeaMothOnUse> seaMothOnUses = new();
        internal static Dictionary<TechType, IVehicleOnEquip> VehicleOnEquips = new();
        internal static Dictionary<TechType, IVehicleOnToggleRepeating> VehicleOnToggleRepeatings = new();
        internal static Dictionary<TechType, IVehicleOnToggleOnce> VehicleOnToggleOnces = new();

        internal static void Patch(Harmony harmony)
        {
            Logger.Log(Logger.Level.Debug, "Vehicle Patches started");
            
            harmony.Patch(AccessTools.Method(typeof(SeaMoth), nameof(SeaMoth.OnUpgradeModuleUse)),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(VehiclePatches), nameof(OnUpgradeModuleUse_Postfix))));

            harmony.Patch(AccessTools.Method(typeof(Vehicle), nameof(Vehicle.OnUpgradeModuleChange)),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(VehiclePatches), nameof(OnUpgradeModuleChange_Postfix))));

            harmony.Patch(AccessTools.Method(typeof(Vehicle), nameof(Vehicle.OnUpgradeModuleToggle)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(VehiclePatches), nameof(OnUpgradeModuleToggle_Prefix))));
            
            Logger.Log(Logger.Level.Debug, "Vehicle Patches done");
        }
        
        static void OnUpgradeModuleUse_Postfix(SeaMoth __instance, TechType techType, int slotID)
        {
            if (seaMothOnUses.TryGetValue(techType, out ISeaMothOnUse seaMothOnUse))
            {
                seaMothOnUse.OnUpgradeUse(slotID, __instance);
                
                __instance.quickSlotTimeUsed[slotID] = Time.time;
                __instance.quickSlotCooldown[slotID] = seaMothOnUse.UseCooldown;
            }
        }
        
        static void OnUpgradeModuleChange_Postfix(Vehicle __instance, TechType techType, int slotID, bool added)
        {
            if (VehicleOnEquips.TryGetValue(techType, out IVehicleOnEquip seaMothOnEquip))
            {
                seaMothOnEquip.OnEquip(slotID, added, __instance);
            }
        }
        
        static bool OnUpgradeModuleToggle_Prefix(Vehicle __instance, int slotID, bool active)
        {
            var techType = __instance.modules.GetTechTypeInSlot(__instance.slotIDs[slotID]);

            if (VehicleOnToggleOnces.TryGetValue(techType, out IVehicleOnToggleOnce vehicleOnToggleOnce))
            {
                vehicleOnToggleOnce.OnToggleOnce(slotID, active, __instance);

                __instance.quickSlotTimeUsed[slotID] = Time.time;
            }
            if (VehicleOnToggleRepeatings.TryGetValue(techType, out IVehicleOnToggleRepeating toggleRepeating))
            {
                var onToggles = __instance.gameObject.GetAllComponentsInChildren<VehicleOnToggleRepeating>();
                foreach (var toggle in onToggles)
                {
                    // if the component already exists on the Vehicle, then skip adding it again
                    if (toggle.techType == techType)
                    {
                        toggle.enabled = active;
                        return false;
                    }
                }
                var onToggle = __instance.gameObject.AddComponent<VehicleOnToggleRepeating>();
                onToggle.vehicleOnToggle = toggleRepeating;
                onToggle.vehicle = __instance;
                onToggle.slotID = slotID;
                onToggle.techType = techType;
                onToggle.enabled = active;
                
                return false;
            }

            return true;
        }
    }
}