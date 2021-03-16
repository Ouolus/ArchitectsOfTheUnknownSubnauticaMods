using HarmonyLib;
using ArchitectsLibrary.MonoBehaviours;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(SeaEmperorBaby))]
    class SeaEmperorBaby_Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeaEmperorBaby.Teleport))]
        static bool Teleport_Patch(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeaEmperorBaby.SetTeleporterTarget))]
        static bool SetTeleporterTarget_Patch(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeaEmperorBaby.SwimToTeleporter))]
        static bool SwimToTeleporter_Patch(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeaEmperorBaby.SwimToMother))]
        static bool SwimToMother_Patch(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
}
