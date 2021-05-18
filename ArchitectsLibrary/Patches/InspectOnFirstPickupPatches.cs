using UnityEngine;
using HarmonyLib;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Patches
{
    class InspectOnFirstPickupPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var target = AccessTools.Method(typeof(InspectOnFirstPickup), "OnInspectObjectBegin");
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(InspectOnFirstPickupPatches), nameof(InspectOnFirstPickup_InspectBegin_Patch)));
            harmony.Patch(target, postfix: postfix);

            var target2 = AccessTools.Method(typeof(InspectOnFirstPickup), "OnInspectObjectDone");
            var postfix2 = new HarmonyMethod(AccessTools.Method(typeof(InspectOnFirstPickupPatches), nameof(InspectOnFirstPickup_InspectEnd_Patch)));
            harmony.Patch(target2, postfix: postfix2);

        }

        public static void InspectOnFirstPickup_InspectBegin_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == AUHandler.PrecursorAlloyIngotTechType)
            {
                __instance.gameObject.transform.GetChild(0).localScale = Vector3.one * 0.59f;
                __instance.gameObject.transform.GetChild(0).localEulerAngles = new Vector3(0f, 90f, 0f);
                __instance.gameObject.transform.GetChild(0).localPosition = new Vector3(-0.21f, 0f, 0f);
            }
        }

        public static void InspectOnFirstPickup_InspectEnd_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == AUHandler.PrecursorAlloyIngotTechType)
            {
                __instance.gameObject.transform.GetChild(0).localScale = Vector3.one;
                __instance.gameObject.transform.GetChild(0).localEulerAngles = Vector3.zero;
                __instance.gameObject.transform.GetChild(0).localPosition = new Vector3(0.13f, -0.06f, -0.36f);
            }
        }
    }
}
