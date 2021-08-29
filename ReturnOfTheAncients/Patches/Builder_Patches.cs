namespace RotA.Patches
{
    using HarmonyLib;
    using Mono.Creatures.GargEssentials;
    using UnityEngine;
    
    [HarmonyPatch(typeof(Builder))]
    internal static class Builder_Patches
    {
        [HarmonyPatch(nameof(Builder.CanDestroyObject))]
        [HarmonyPrefix]
        private static bool CanDestroyObjectPrefix(GameObject go, ref bool __result)
        {
            if (go.GetComponentInParent<GargantuanBehaviour>() != null)
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}