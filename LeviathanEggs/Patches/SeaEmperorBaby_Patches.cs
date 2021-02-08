using HarmonyLib;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.Teleport))]
    class SeaEmperorBaby_Teleport_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
    [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SetTeleporterTarget))]
    class SeaEmperorBaby_SetTeleporterTarget_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
    [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToTeleporter))]
    class SeaEmperorBaby_SwimToTeleporter_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
    [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToMother))]
    class SeaEmperorBaby_SwimToMother_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
}
