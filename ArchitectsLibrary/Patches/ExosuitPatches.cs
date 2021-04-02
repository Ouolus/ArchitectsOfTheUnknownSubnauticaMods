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