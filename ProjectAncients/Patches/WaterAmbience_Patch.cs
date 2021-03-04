using ECCLibrary;
using HarmonyLib;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    class WaterAmbience_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterAmbience), nameof(WaterAmbience.Start))]
        public static void Start_Postfix(WaterAmbience __instance)
        {
            GameObject ambienceParent = __instance.gameObject.SearchChild("background");
            GameObject voidAmbience = GameObject.Instantiate(ambienceParent.SearchChild("arcticAmbience"), ambienceParent.transform);
            voidAmbience.name = "voidAmbience";
            voidAmbience.GetComponent<FMODGameParams>().onlyInBiome = "void";
        }
    }
}
