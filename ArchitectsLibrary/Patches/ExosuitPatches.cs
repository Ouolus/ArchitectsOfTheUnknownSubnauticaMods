using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public class ExosuitPatches
    {
        internal static Dictionary<TechType, IExosuitOnEquip> ExosuitOnEquips = new();
        internal static Dictionary<TechType, IExosuitOnToggle> ExosuitOnToggle = new();

        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleToggle))]
        [HarmonyPostfix]
        static void OnUpgradeModuleToggle_Postfix(Exosuit __instance, bool active, int slotID)
        {
            InventoryItem item = __instance.GetSlotItem(slotID);
            if(item is null)
            {
                return;
            }
            if (ExosuitOnToggle.TryGetValue(item.item.GetTechType(), out IExosuitOnToggle exosuitOnToggle))
            {
                exosuitOnToggle.OnToggle(slotID, active, __instance);

                __instance.quickSlotTimeUsed[slotID] = Time.time;
            }
        }

        [HarmonyPatch(nameof(Exosuit.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        static void OnUpgradeModuleChange(Exosuit __instance, TechType techType, int slotID)
        {
            if (ExosuitOnEquips.TryGetValue(techType, out IExosuitOnEquip exosuitOnEquip))
            {
                exosuitOnEquip.OnEquip(slotID, __instance);
            }
        }
    }
}