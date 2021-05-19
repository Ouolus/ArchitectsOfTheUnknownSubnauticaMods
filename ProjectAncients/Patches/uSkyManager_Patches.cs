using HarmonyLib;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(uSkyManager))]
    public class uSkyManager_Patches
    {
        [HarmonyPatch(nameof(uSkyManager.PlanetPos))]
        [HarmonyPrefix]
        public static bool PlanetPosPrefix(ref Vector3 __result)
        {
            if (DayNightCycle.main != null && uGUI.isMainLevel)
            {
                return true;
            }
            else
            {
                __result = new Vector3(200f, 300f, 10000f); //this was hard to get right
                return false;
            }
        }
    }
}
