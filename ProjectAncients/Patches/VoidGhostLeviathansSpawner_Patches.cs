using HarmonyLib;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    class VoidGhostLeviathansSpawner_Patches
    {
        [HarmonyPrefix] //Prefix because ghost leviathans can technically spawn during the Start method.
        [HarmonyPatch(typeof(VoidGhostLeviathansSpawner), nameof(VoidGhostLeviathansSpawner.Start))]
        public static void Start_Prefix(VoidGhostLeviathansSpawner __instance) //This method modifies the void ghost leviathans, so the Gargantuan can target them.
        {
            var ecoTarget = __instance.ghostLeviathanPrefab.EnsureComponent<EcoTarget>();
            ecoTarget.type = EcoTargetType.Leviathan;
        }
    }
}
