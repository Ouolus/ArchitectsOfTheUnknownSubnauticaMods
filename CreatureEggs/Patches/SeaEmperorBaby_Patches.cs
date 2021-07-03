using HarmonyLib;
using ArchitectsLibrary.MonoBehaviours;
namespace CreatureEggs.Patches
{
    [HarmonyPatch]
    class SeaEmperorBaby_Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.Teleport))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SetTeleporterTarget))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToTeleporter))]
        [HarmonyPatch(typeof(SeaEmperorBaby), nameof(SeaEmperorBaby.SwimToMother))]
        static bool Prefix(SeaEmperorBaby __instance)
        {
            return __instance.gameObject.GetComponent<StagedGrowing>() == null;
        }
    }
}
