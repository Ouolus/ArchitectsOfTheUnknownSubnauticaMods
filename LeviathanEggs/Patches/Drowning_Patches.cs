using HarmonyLib;

namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Drowning))]
    class Drowning_Patches
    {
        [HarmonyPatch(nameof(Drowning.Update))]
        [HarmonyPrefix]
        static bool UpdatePrefix(Drowning __instance)
        {
            var techType = CraftData.GetTechType(__instance.gameObject);

            if (techType == TechType.Skyray)
                return false;

            return true;
        }
    }
}