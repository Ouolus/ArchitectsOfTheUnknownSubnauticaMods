using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    internal class UpgradeConsolePatches
    {
        internal static readonly Dictionary<TechType, ICyclopsOnEquip> CyclopsOnEquips = new();

        internal static void Patch(Harmony harmony)
        {            
            harmony.Patch(AccessTools.Method(typeof(UpgradeConsole), nameof(UpgradeConsole.OnEquip)),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(UpgradeConsolePatches), nameof(OnEquip_Postfix))));

            harmony.Patch(AccessTools.Method(typeof(UpgradeConsole), nameof(UpgradeConsole.OnUnequip)),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(UpgradeConsolePatches), nameof(OnUnequip_Postfix))));
        }

        static void OnEquip_Postfix(UpgradeConsole __instance, string slot, InventoryItem item)
        {
            if (item == null || item.item == null) return;
            if (CyclopsOnEquips.TryGetValue(item.item.GetTechType(), out ICyclopsOnEquip cyclopsOnEquip))
            {
                SubRoot subRoot = __instance.GetComponentInParent<SubRoot>();
                if (subRoot)
                {
                    cyclopsOnEquip.OnEquip(slot, true, subRoot);
                }
            }
        }

        static void OnUnequip_Postfix(UpgradeConsole __instance, string slot, InventoryItem item)
        {
            if (item == null || item.item == null) return;
            if (CyclopsOnEquips.TryGetValue(item.item.GetTechType(), out ICyclopsOnEquip cyclopsOnEquip))
            {
                SubRoot subRoot = __instance.GetComponentInParent<SubRoot>();
                if (subRoot)
                {
                    cyclopsOnEquip.OnEquip(slot, false, subRoot);
                }
            }
        }
    }
}