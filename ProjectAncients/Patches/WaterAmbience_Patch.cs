using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    class VoidAmbience_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterAmbience), nameof(WaterAmbience.Start))]
        public static void WaterAmbience_Start_Postfix(WaterAmbience __instance)
        {
            GameObject ambienceParent = __instance.gameObject.SearchChild("background");
            GameObject voidAmbience = GameObject.Instantiate(ambienceParent.SearchChild("arcticAmbience"), ambienceParent.transform);
            voidAmbience.name = "voidAmbience";
            voidAmbience.GetComponent<FMODGameParams>().onlyInBiome = "void";
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.Start))]
        public static void WaterBiomeManager_Start_Postfix(WaterBiomeManager __instance)
        {
            if (!__instance.biomeLookup.ContainsKey("void"))
            {
                WaterscapeVolume.Settings waterscapeSettings = new WaterscapeVolume.Settings()
                {
                    absorption = new Vector3(125f, 20f, 4f),
                    ambientScale = 0f,
                    emissiveScale = 0f,
                    sunlightScale = 1f,
                    murkiness = 0.1f,
                    startDistance = 50f,
                    scatteringColor = Color.black,
                    temperature = 0f,
                    scattering = 0f
                };
                WaterBiomeManager.BiomeSettings biomeSettings = new WaterBiomeManager.BiomeSettings()
                {
                    name = "void",
                    skyPrefab = null,
                    settings = waterscapeSettings
                };
                __instance.biomeSettings.Add(biomeSettings);
                int indexForNew = __instance.biomeSettings.Count - 1;
                __instance.biomeLookup.Add("void", indexForNew);
            }
        }
    }
}
