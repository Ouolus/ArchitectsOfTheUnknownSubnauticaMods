using System;
using HarmonyLib;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(EnergyMixin), nameof(EnergyMixin.GetText))]
    internal static class EnergyMixin_Patches
    {
        [HarmonyPrefix]
        private static bool Prefix(EnergyMixin __instance, ref string __result)
        {
            var tt = CraftData.GetTechType(__instance.gameObject);
            if (tt == Mod.ionKnife.TechType)
            {
                __result = String.Empty;
                return false;
            }

            return true;
        }
    }
}