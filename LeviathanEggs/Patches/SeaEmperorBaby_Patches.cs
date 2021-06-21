using HarmonyLib;
using ArchitectsLibrary.MonoBehaviours;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch]
    class SeaEmperorBaby_Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.Teleport))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SetTeleporterTarget))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToTeleporter))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToMother))]
        static bool Teleport_Patch(SeaEmperorBaby __instance)
        {
            if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                return false;

            return true;
        }
    }
}
