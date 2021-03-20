using ECCLibrary;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    public static class PrecursorTabletFixer
    {
        [HarmonyPatch(typeof(InspectOnFirstPickup), nameof(InspectOnFirstPickup.Start))]
        [HarmonyPostfix]
        public static void InspectOnFirstPickup_Start_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == TechType.PrecursorKey_White || techTag.type == TechType.PrecursorKey_Red)
            {
                BoxCollider boxCol = __instance.pickupAble.GetComponent<BoxCollider>();
                if (boxCol)
                {
                    boxCol.isTrigger = false;
                }
            }
        }
    }
}
